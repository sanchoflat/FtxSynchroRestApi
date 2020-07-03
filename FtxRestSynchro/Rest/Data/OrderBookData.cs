namespace FtxRestSynchro.Rest.Data
{
    public class OrderBookData
    {
        public decimal Price;
        public decimal Amount;

        public OrderBookData(decimal price, decimal amount)
        {
            Price = price;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{Price} - {Amount}";
        }
    }
}