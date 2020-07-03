namespace FtxRestSynchro.Rest.Data
{
    public class OrderBookDepth : AbstractRestData
    {
        public OrderBookData[] Asks;
        public OrderBookData[] Bids;

        public OrderBookDepth(OrderBookData[] bids, OrderBookData[] asks)
        {
            Asks = asks;
            Bids = bids;
        }

        public OrderBookDepth(string error, int errorCode) : base(error, errorCode)
        {
        }

        public OrderBookDepth(int errorCode) : base(errorCode)
        {
        }

        public override string ToString()
        {
            if (HasError) return base.ToString();
            return $"Asks: {Asks.Length}, Bids: {Bids.Length}";
        }
    }
}