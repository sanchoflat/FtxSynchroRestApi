using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FtxRestSynchro.Enums;
using FtxRestSynchro.Rest.Data;
using FtxRestSynchro.Rest.Parsers;

namespace FtxRestSynchro
{
    public class FtxRestApi
    {
        protected ExchangeCredentials _credentials;

        protected HttpClient _httpClient;

        private const string Url = "https://ftx.com/";

        private readonly HMACSHA256 _hashMaker;

        private long _nonce;

        public FtxRestApi(ExchangeCredentials credentials)
        {
            _credentials = credentials;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Url),
                Timeout = TimeSpan.FromSeconds(60)
            };

            _httpClient.DefaultRequestHeaders.ConnectionClose = false;
            _hashMaker = new HMACSHA256(Encoding.UTF8.GetBytes(_credentials.ApiSecret));

            var subAccount = credentials.Subaccount;
            if (!string.IsNullOrEmpty(subAccount))
            {
                _httpClient.DefaultRequestHeaders.Add("FTX-SUBACCOUNT", WebUtility.UrlEncode(subAccount));
            }

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public OrderBookDepth GetOrderBookDepth(string symbol, int limit = 5)
        {
            var resultString = $"api/markets/{symbol}/orderbook?depth={limit}";
            var result = Call(HttpMethod.Get, resultString);
            var res = FtxRestDataParser.ParseOrderBookDepth(result, limit);
            return res;
        }

        public PlaceOrderInfo PlaceOrder(string market, SideType side, decimal? price, OrderType orderType, decimal size, bool reduceOnly, bool ioc, bool postOnly, string clientOrderId)
        {
            var path = $"api/orders";
            var priceStr = (price == null || orderType == OrderType.Market) ? "null" : price.ToString();
            var body = "";
            if (string.IsNullOrEmpty(clientOrderId))
            {
                body = $"{{\"market\": \"{market}\"," +
                       $"\"side\": \"{side.ToString().ToLower()}\"," +
                       $"\"price\": {priceStr}," +
                       $"\"type\": \"{orderType.ToString().ToLower()}\"," +
                       $"\"size\": {size}," +
                       $"\"reduceOnly\": {reduceOnly.ToString().ToLower()}," +
                       $"\"ioc\": {ioc.ToString().ToLower()}," +
                       $"\"postOnly\": {postOnly.ToString().ToLower()}}}";
            }
            else
            {
                body = $"{{\"market\": \"{market}\"," +
                       $"\"side\": \"{side.ToString().ToLower()}\"," +
                       $"\"price\": {priceStr}," +
                       $"\"type\": \"{orderType.ToString().ToLower()}\"," +
                       $"\"size\": {size}," +
                       $"\"clientId\": {clientOrderId}," +
                       $"\"reduceOnly\": {reduceOnly.ToString().ToLower()}," +
                       $"\"ioc\": {ioc.ToString().ToLower()}," +
                       $"\"postOnly\": {postOnly.ToString().ToLower()}}}";
            }

            var sign = GenerateSignature(HttpMethod.Post, "/api/orders", body);
            var result = CallSign(HttpMethod.Post, path, sign, body);
            var res = FtxRestDataParser.ParseOrderInfo(result);
            return res;
        }

        public PlaceOrderInfo EditOrder(string orderId, string clientId, decimal price, decimal size)
        {
            var path = $"api/orders/{orderId}/modify";

            var body = "{";
            if (!string.IsNullOrEmpty(clientId))
            {
                body += $"\"clientId\": {clientId}";
            }
            body += $"\"price\": {price},";
            body += $"\"size\": {size}}}";

            var sign = GenerateSignature(HttpMethod.Post, $"/{path}", body);
            var result = CallSign(HttpMethod.Post, path, sign, body);
            var res = FtxRestDataParser.ParseOrderInfo(result);
            return res;
        }

        public CancelOrderInfo CancelOrder(string id)
        {
            var resultString = $"api/orders/{id}";
            var sign = GenerateSignature(HttpMethod.Delete, $"/api/orders/{id}", "");
            var result = CallSign(HttpMethod.Delete, resultString, sign);
            var res = FtxRestDataParser.ParseCancelOrderInfo(result);

            return res;
        }

        public AccountInfo GetAccountInfo()
        {
            var resultString = $"api/account";

            var sign = GenerateSignature(HttpMethod.Get, "/api/account", "");
            var result = CallSign(HttpMethod.Get, resultString, sign);

            var res = FtxRestDataParser.ParseAccountInfo(result);
            return res;
        }

        #region Util

        private string Call(HttpMethod method, string endpoint)
        {
            var request = new HttpRequestMessage(method, endpoint);

            HttpResponseMessage response = null;
            var result = "";

            try
            {
                response = _httpClient.SendAsync(request).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                return "{\"error\":\"request error\",\"success\":false}";
            }

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            if (result.Contains("DOCTYPE") || result.Contains("Bad Gateway") || result.Contains("Service Temporarily Unavailable") || result.Contains("<html"))
            {
                return "{\"error\":\"html error\",\"success\":false}";
            }

            return result;
        }

        private string CallSign(HttpMethod method, string endpoint, string sign, string body = null)
        {
            var request = new HttpRequestMessage(method, endpoint);

            if (body != null)
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            request.Headers.Add("FTX-KEY", _credentials.ApiKey);
            request.Headers.Add("FTX-SIGN", sign);
            request.Headers.Add("FTX-TS", _nonce.ToString());

            HttpResponseMessage response = null;
            var result = "";

            try
            {
                response = _httpClient.SendAsync(request).Result;
                result = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                return "{\"error\":\"request error\",\"success\":false}";
            }

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            if (result.Contains("DOCTYPE") || result.Contains("Bad Gateway") || result.Contains("Service Temporarily Unavailable") || result.Contains("<html"))
            {
                return "{\"error\":\"html error\",\"success\":false}";
            }

            return result;
        }

        private string GenerateSignature(HttpMethod method, string url, string requestBody)
        {
            _nonce = GetNonce();
            var signature = $"{_nonce}{method.ToString().ToUpper()}{url}{requestBody}";
            var hash = _hashMaker.ComputeHash(Encoding.UTF8.GetBytes(signature));
            var hashStringBase64 = BitConverter.ToString(hash).Replace("-", string.Empty);
            return hashStringBase64.ToLower();
        }
        
        private static DateTime EpochTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        
        private long GetNonce()
        {
            return (long)(DateTime.UtcNow - EpochTime).TotalMilliseconds;
        }

        #endregion
    }
}