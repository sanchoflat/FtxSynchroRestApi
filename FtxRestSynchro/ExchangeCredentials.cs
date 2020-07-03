namespace FtxRestSynchro
{
    public class ExchangeCredentials
    {
        public string Subaccount { get; }

        public string ApiKey { get; }

        public string ApiSecret { get; }

        public ExchangeCredentials()
        {
            ApiKey = "";
            ApiSecret = "";
        }

        public ExchangeCredentials(string apiKey, string apiSecret)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
        }

        public ExchangeCredentials(string apiKey, string apiSecret, string subaccount)
        {
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            Subaccount = subaccount;
        }
    }
}