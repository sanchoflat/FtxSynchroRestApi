namespace FtxRestSynchro.Rest.Parsers
{
    public static class FtxErrorParser
    {

        public static int ParseError(string errorName)
        {
            if (errorName.Contains("try again"))
            {
                return FtxErrors.TryAgain;
            }

            if (errorName.Contains("not logged in"))
            {
                return FtxErrors.NotLoggedIn;
            }

            if (errorName.Contains("order already closed"))
            {
                return FtxErrors.OrderAlreadyClosed;
            }

            if (errorName.Contains("account does not have enough margin"))
            {
                return FtxErrors.NotEnoughMargin;
            }

            if (errorName.Contains("order not found"))
            {
                return FtxErrors.OrderNotFound;
            }

            if (errorName.Contains("size too small"))
            {
                return FtxErrors.SizeToSmall;
            }

            if (errorName.Contains("duplicate client order id"))
            {
                return FtxErrors.DuplicateClientOrderId;
            }

            if (errorName.Contains("timeoutexception"))
            {
                return FtxErrors.TimeoutException;
            }

            if (errorName.Contains("rate limit exceeded"))
            {
                return FtxErrors.RateLimit;
            }

            if (errorName.Contains("html"))
            {
                return FtxErrors.HtmlError;
            }

            if (errorName.Contains("429"))
            {
                return FtxErrors.HtmlError;
            }

            if (errorName.Contains("request error"))
            {
                return FtxErrors.RequestError;
            }

            return FtxErrors.Invalid;
        }
    }
}