namespace FtxRestSynchro.Rest.Parsers
{
    public struct RequestError
    {
        public RequestError(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }

        public string Message { get; set; }

        public bool ErrorAvaliable => Code > 0;
    }
}