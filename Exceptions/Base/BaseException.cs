namespace M10Backend.Exceptions.Base
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string mess, int statusCode) : base(mess)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
