namespace FtxRestSynchro.Rest.Parsers
{
    public static class FtxErrors
    {
        public const int NotEnoughMargin = 1;
        public const int TryAgain = 2;
        public const int NotLoggedIn = 3;
        public const int OrderAlreadyClosed = 4;
        public const int OrderNotFound = 5;
        public const int SizeToSmall = 6;
        public const int DuplicateClientOrderId = 7;
        public const int TimeoutException = 8;
        public const int RateLimit = 9;
        public const int HtmlError = 997;
        public const int RequestError = 998;
        public const int Invalid = 999;
    }
}