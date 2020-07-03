using System;

namespace FtxRestSynchro.Rest.Data
{
    public abstract class AbstractRestData
    {
        public string Error { get; protected set; }
        public int ErrorCode { get; protected set; }
        public bool HasError => ErrorCode > 0;

        protected AbstractRestData(int errorCode)
        {
            ErrorCode = errorCode;
        }

        protected AbstractRestData(string error, int errorCode)
        {
            Error = error;
            ErrorCode = Math.Abs(errorCode);
        }

        protected AbstractRestData(string error)
        {
            Error = error;
        }

        protected AbstractRestData()
        {
        }

        public override string ToString()
        {
            return $"{nameof(Error)}: {Error}, {nameof(ErrorCode)}: {ErrorCode}";
        }
    }
}