using System;
using FtxRestSynchro.Enums;

namespace FtxRestSynchro.Rest.Data
{
    public class CancelOrderInfo : AbstractRestData
    {
        public CancelOrderInfo() { }

        public CancelOrderInfo(string error, int errorCode) : base(error, errorCode) { }

        public bool Success { get; set; }
        public string Result { get; set; }
    }

    public class PlaceOrderInfo : AbstractRestData
    {
        public PlaceOrderInfo() { }

        public PlaceOrderInfo(string error, int errorCode) : base(error, errorCode) { }

        public decimal AvgFillPrice { get; set; }
        public string ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal FilledSize { get; set; }
        public string Future { get; set; }
        public string Id { get; set; }
        public bool Ioc { get; set; }
        public bool Liquidation { get; set; }
        public string Market { get; set; }
        public bool PostOnly { get; set; }
        public decimal Price { get; set; }
        public bool ReduceOnly { get; set; }
        public decimal RemainingSize { get; set; }
        public SideType Side { get; set; }
        public decimal Size { get; set; }
        public OrderStatus Status { get; set; }
        public OrderType Type { get; set; }
    }
}