using System.Collections.Generic;

namespace FtxRestSynchro.Rest.Data
{
    public class AccountInfo : AbstractRestData
    {
        public class AccountPosition
        {
            public decimal CollateralUsed { get; set; }
            public decimal Cost { get; set; }
            public decimal EntryPrice { get; set; }
            public decimal EstimatedLiquidationPrice { get; set; }
            public string Future { get; set; }
            public decimal InitialMarginRequirement { get; set; }
            public decimal LongOrderSize { get; set; }
            public decimal MaintenanceMarginRequirement { get; set; }
            public decimal NetSize { get; set; }
            public decimal OpenSize { get; set; }
            public decimal RealizedPnl { get; set; }
            public decimal ShortOrderSize { get; set; }
            public string Side { get; set; }
            public decimal Size { get; set; }
            public decimal UnrealizedPnl { get; set; }
        }

        public AccountInfo(string error, int errorCode) : base(error, errorCode) { }

        public AccountInfo()
        {
        }

        public bool BackstopProvider { get; set; }
        public bool ChargeInterestOnNegativeUsd { get; set; }
        public decimal Collateral { get; set; }
        public decimal FreeCollateral { get; set; }
        public decimal InitialMarginRequirement { get; set; }
        public decimal Leverage { get; set; }
        public bool Liquidating { get; set; }
        public decimal MaintenanceMarginRequirement { get; set; }
        public decimal MakerFee { get; set; }
        public decimal MarginFraction { get; set; }
        public decimal OpenMarginFraction { get; set; }
        public IList<AccountPosition> Positions { get; set; }
        public decimal TakerFee { get; set; }
        public decimal TotalAccountValue { get; set; }
        public decimal TotalPositionSize { get; set; }
        public bool UseFttCollateral { get; set; }
        public string Username { get; set; }
    }
}