using FtxRestSynchro.Enums;

namespace FtxRestSynchro
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new FtxRestApi(new ExchangeCredentials(apiKey:"", apiSecret:"", subaccount:""));

            var orderBookDepth = api.GetOrderBookDepth("BTC-PERP", 10);

            var accountInfo = api.GetAccountInfo();

            var placedOrder = api.PlaceOrder("BTC-PERP", SideType.Buy, 5000, OrderType.Limit, 0.001m, false, false,
                true, null);

            var editOrder = api.EditOrder(placedOrder.Id, null, 5001, 0.0015m);
            var editOrder1 = api.EditOrder(editOrder.Id, null, 4999, 0.001m);
            var editOrder2 = api.EditOrder(editOrder1.Id, null, 5002, 0.0017m);

            var canceled = api.CancelOrder(editOrder2.Id);
        }
    }
}