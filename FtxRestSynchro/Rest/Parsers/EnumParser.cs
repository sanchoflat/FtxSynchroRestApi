using System;
using FtxRestSynchro.Enums;
using Newtonsoft.Json.Linq;

namespace FtxRestSynchro.Rest.Parsers
{
    public static class EnumParser
    {
        public static OrderStatus ParseOrderStatus(JToken token)
        {
            return ParseOrderStatus(token.ToString());
        }

        public static OrderStatus ParseOrderStatus(string status)
        {
            switch (status.ToUpper())
            {
                case "NEW":
                    return OrderStatus.New;
                case "OPEN":
                    return OrderStatus.Open;
                case "CLOSED":
                    return OrderStatus.Closed;
            }

            throw new NotImplementedException($"ParseOrderStatus: {status}");
        }

        public static OrderType ParseOrderType(JToken token)
        {
            return ParseOrderType(token.ToString());
        }

        public static OrderType ParseOrderType(string orderType)
        {
            switch (orderType.ToUpper())
            {
                case "LIMIT":
                    return OrderType.Limit;
                case "MARKET":
                    return OrderType.Market;
            }

            throw new NotImplementedException($"ParseOrderType: {orderType}");
        }

        public static SideType ParseSide(JToken token)
        {
            return ParseSide(token.ToString());
        }

        public static SideType ParseSide(string side)
        {
            switch (side)
            {
                case "buy":
                    return SideType.Buy;
                case "sell":
                    return SideType.Sell;
            }
            throw new NotImplementedException();
        }
    }
}