using M10Backend.Exceptions.Base;

namespace M10Backend.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException(string mess = "Entity already exists", int statusCode = 400) : base(mess, statusCode)
        {

        }
    }
}
