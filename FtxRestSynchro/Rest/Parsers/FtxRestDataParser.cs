using System.Collections.Generic;
using FtxRestSynchro.Rest.Data;
using Newtonsoft.Json.Linq;

namespace FtxRestSynchro.Rest.Parsers
{
    public static class FtxRestDataParser
    {
        public static OrderBookDepth ParseOrderBookDepth(string data, int limit)
        { 
            /*
           {
               "result": {
                   "asks": [[8474.0, 2.5738], [8476.5, 2.6028]],
                   "bids": [[8473.5, 1.1804], [8473.0, 73.913]]
               },
               "success": true
           }
           */

            var token = ParserHelpers.Parse(data);
            var errorRes = ParseErrors(token);
            if (errorRes.ErrorAvaliable)
            {
                return new OrderBookDepth(errorRes.Message, errorRes.Code);
            }

            token = token["result"];
            var asks = new OrderBookData[limit];
            var bids = new OrderBookData[limit];

            var askToken = token["asks"];
            var bidToken = token["bids"];

            int counter = 0;
            foreach (var ask in askToken)
            {
                var p = ParserHelpers.ParseDecimal(ask[0]);
                var a = ParserHelpers.ParseDecimal(ask[1]);

                asks[counter] = new OrderBookData(p, a);
                counter++;
            }

            counter = 0;
            foreach (var bid in bidToken)
            {
                var p = ParserHelpers.ParseDecimal(bid[0]);
                var a = ParserHelpers.ParseDecimal(bid[1]);

                bids[counter] = new OrderBookData(p, a);
                counter++;
            }

            return new OrderBookDepth(bids, asks);
        }

        public static AccountInfo ParseAccountInfo(string result)
        {
            /*
            {
                "result": {
                    "backstopProvider": false,
                    "chargeInterestOnNegativeUsd": true,
                    "collateral": 682192.2800554956,
                    "freeCollateral": 520667.73680795176,
                    "initialMarginRequirement": 0.06967024123125505,
                    "leverage": 20.0,
                    "liquidating": false,
                    "maintenanceMarginRequirement": 0.04182233133201594,
                    "makerFee": -0.0001,
                    "marginFraction": 0.3164937566252949,
                    "openMarginFraction": 0.3104009884200521,
                    "positionLimit": null,
                    "positionLimitUsed": null,
                    "positions": [{
                            "collateralUsed": 624.8008875,
                            "cost": -12506.597,
                            "entryPrice": 6.502,
                            "estimatedLiquidationPrice": 314.82111921281233,
                            "future": "ETC-PERP",
                            "initialMarginRequirement": 0.05,
                            "longOrderSize": 161.7,
                            "maintenanceMarginRequirement": 0.03,
                            "netSize": -1923.5,
                            "openSize": 1923.5,
                            "realizedPnl": 1008.65163425,
                            "shortOrderSize": 0.0,
                            "side": "sell",
                            "size": 1923.5,
                            "unrealizedPnl": 10.57925
                        }, {
                            "collateralUsed": 516.531463875,
                            "cost": -10342.370535,
                            "entryPrice": 0.015415,
                            "estimatedLiquidationPrice": 0.8993396236164252,
                            "future": "TRX-PERP",
                            "initialMarginRequirement": 0.05,
                            "longOrderSize": 0.0,
                            "maintenanceMarginRequirement": 0.03,
                            "netSize": -670929.0,
                            "openSize": 670929.0,
                            "realizedPnl": -7183.70970352,
                            "shortOrderSize": 0.0,
                            "side": "sell",
                            "size": 670929.0,
                            "unrealizedPnl": 11.7412575
                        }
                    ],
                    "takerFee": 0.00015,
                    "totalAccountValue": 683363.9439884955,
                    "totalPositionSize": 2159170.3775615,
                    "useFttCollateral": true,
                    "username": "auth0|5d66431d23ce5f0d5693e37f/Trade_Algo"
                },
                "success": true
            }
            */
           
            var token = ParserHelpers.Parse(result);
            var errorRes = ParseErrors(token);
            if (errorRes.ErrorAvaliable)
            {
                return new AccountInfo(errorRes.Message, errorRes.Code);
            }

            token = token["result"];
            var backstopProvider = ParserHelpers.ParseBool(token["backstopProvider"]);
            var chargeInterestOnNegativeUsd = ParserHelpers.ParseBool(token["chargeInterestOnNegativeUsd"]);
            var collateral = ParserHelpers.ParseDecimal(token["collateral"]);
            var freeCollateral = ParserHelpers.ParseDecimal(token["freeCollateral"]);
            var initialMarginRequirement = ParserHelpers.ParseDecimal(token["initialMarginRequirement"]);
            var leverage = ParserHelpers.ParseDecimal(token["leverage"]);
            var liquidating = ParserHelpers.ParseBool(token["liquidating"]);
            var maintenanceMarginRequirement = ParserHelpers.ParseDecimal(token["maintenanceMarginRequirement"]);
            var makerFee = ParserHelpers.ParseDecimal(token["makerFee"]);
            var marginFraction = ParserHelpers.ParseDecimal(token["marginFraction"]);
            var openMarginFraction = ParserHelpers.ParseDecimal(token["openMarginFraction"]);
            var takerFee = ParserHelpers.ParseDecimal(token["takerFee"]);
            var totalAccountValue = ParserHelpers.ParseDecimal(token["totalAccountValue"]);
            var totalPositionSize = ParserHelpers.ParseDecimal(token["totalPositionSize"]);
            var useFttCollateral = ParserHelpers.ParseBool(token["useFttCollateral"]);
            var username = ParserHelpers.ParseString(token["username"]);


            var positions = token["positions"];
            var positionsList = new List<AccountInfo.AccountPosition>();
            foreach (var pos in positions)
            {
                var collateralUsed = ParserHelpers.ParseDecimal(pos["collateralUsed"]);
                var cost = ParserHelpers.ParseDecimal(pos["cost"]);
                var entryPrice = ParserHelpers.ParseDecimal(pos["entryPrice"]);
                var estimatedLiquidationPrice = ParserHelpers.ParseDecimal(pos["estimatedLiquidationPrice"]);
                var posInitialMarginRequirement = ParserHelpers.ParseDecimal(pos["initialMarginRequirement"]);
                var longOrderSize = ParserHelpers.ParseDecimal(pos["longOrderSize"]);
                var posMaintenanceMarginRequirement = ParserHelpers.ParseDecimal(pos["maintenanceMarginRequirement"]);
                var netSize = ParserHelpers.ParseDecimal(pos["netSize"]);
                var openSize = ParserHelpers.ParseDecimal(pos["openSize"]);
                var realizedPnl = ParserHelpers.ParseDecimal(pos["realizedPnl"]);
                var shortOrderSize = ParserHelpers.ParseDecimal(pos["shortOrderSize"]);
                var size = ParserHelpers.ParseDecimal(pos["size"]);
                var unrealizedPnl = ParserHelpers.ParseDecimal(pos["unrealizedPnl"]);
                
                var future = ParserHelpers.ParseString(pos["future"]);
                var side = ParserHelpers.ParseString(pos["side"]);
                
                var ap = new AccountInfo.AccountPosition()
                {
                    CollateralUsed = collateralUsed,
                    Cost = cost, 
                    EntryPrice = entryPrice,
                    EstimatedLiquidationPrice = estimatedLiquidationPrice,
                    Future = future, 
                    InitialMarginRequirement = posInitialMarginRequirement,
                    LongOrderSize = longOrderSize,
                    MaintenanceMarginRequirement = posMaintenanceMarginRequirement,
                    NetSize = netSize,
                    OpenSize = openSize,
                    RealizedPnl = realizedPnl,
                    ShortOrderSize = shortOrderSize, 
                    Side = side,
                    Size = size,
                    UnrealizedPnl = unrealizedPnl,
                };
                positionsList.Add(ap);
            }

            return new AccountInfo()
            {
                BackstopProvider = backstopProvider, 
                ChargeInterestOnNegativeUsd = chargeInterestOnNegativeUsd,
                Collateral = collateral,
                FreeCollateral = freeCollateral,
                InitialMarginRequirement = initialMarginRequirement,
                Leverage = leverage,
                Liquidating = liquidating,
                MaintenanceMarginRequirement = maintenanceMarginRequirement,
                MakerFee = makerFee,
                MarginFraction = marginFraction,
                OpenMarginFraction = openMarginFraction,
                TotalPositionSize = totalPositionSize,
                Positions = positionsList,
                TakerFee = takerFee,
                TotalAccountValue = totalAccountValue,
                UseFttCollateral = useFttCollateral,
                Username = username
            };
        }

        public static PlaceOrderInfo ParseOrderInfo(string result)
        {
            /*
            {
                "result": {
                    "avgFillPrice": null,
                    "clientId": "545198491015452514510154989745",
                    "createdAt": "2020-04-30T10:31:49.114165+00:00",
                    "filledSize": 0.0,
                    "future": "ADA-PERP",
                    "id": 4976240395,
                    "ioc": false,
                    "liquidation": false,
                    "market": "ADA-PERP",
                    "postOnly": true,
                    "price": 0.048955,
                    "reduceOnly": false,
                    "remainingSize": 27497.0,
                    "side": "sell",
                    "size": 27497.0,
                    "status": "new",
                    "type": "limit"
                },
                "success": true
            }
            */

            var token = ParserHelpers.Parse(result);
            var errorRes = ParseErrors(token);
            if (errorRes.ErrorAvaliable)
            {
                return new PlaceOrderInfo(errorRes.Message, errorRes.Code);
            }

            token = token["result"];
            var future = ParserHelpers.ParseString(token["future"]);
            var market = ParserHelpers.ParseString(token["market"]);
            var orderId = ParserHelpers.ParseString(token["id"]);
            var clientId = ParserHelpers.ParseString(token["clientId"]);
            var side = EnumParser.ParseSide(token["side"]);
            var price = ParserHelpers.ParseDecimal(token["price"], true);
            var size = ParserHelpers.ParseDecimal(token["size"]);
            var filledSize = ParserHelpers.ParseDecimal(token["filledSize"]);
            var remainingSize = ParserHelpers.ParseDecimal(token["remainingSize"]);
            var ioc = ParserHelpers.ParseBool(token["ioc"]);
            var reduceOnly = ParserHelpers.ParseBool(token["reduceOnly"]);
            var liquidation = ParserHelpers.ParseBool(token["liquidation"]);
            var postOnly = ParserHelpers.ParseBool(token["postOnly"]);
            var createdAt = ParserHelpers.ParseDateTime(token["createdAt"]);
            var status = EnumParser.ParseOrderStatus(token["status"]);
            var type = EnumParser.ParseOrderType(token["type"]);

            return new PlaceOrderInfo()
            {
                Size = size,
                Side = side, Future = future,
                AvgFillPrice = filledSize,
                ClientId = clientId,
                FilledSize = filledSize,
                Id = orderId,
                Market = market,
                Price = price,
                Status = status,
                Type = type,
                Ioc = ioc,
                ReduceOnly = reduceOnly,
                Liquidation = liquidation,
                PostOnly = postOnly,
                RemainingSize = remainingSize,
                CreatedAt = createdAt
            };
        }

        public static CancelOrderInfo ParseCancelOrderInfo(string result)
        {
            var token = ParserHelpers.Parse(result);

            var errorRes = ParseErrors(token);
            if (errorRes.ErrorAvaliable)
            {
                return new CancelOrderInfo(errorRes.Message, errorRes.Code);
            }

            var success = ParserHelpers.ParseBool(token["success"]);
            var res = ParserHelpers.ParseString(token["result"]);

            return new CancelOrderInfo(){ Result = res, Success = success};
        }

        public static RequestError ParseErrors(JToken token)
        {
            try
            {
                var t = token.SelectToken("error");
                if (t != null)
                {
                    var errorName = ParserHelpers.ParseString(t).ToLower();

                    return new RequestError(FtxErrorParser.ParseError(errorName), errorName);
                }
            }
            catch { }

            return new RequestError();
        }
    }
}