using M10Backend.Exceptions.Base;

namespace M10Backend.Exceptions.Auth
{
   
    public class UserCreateException : BaseException
    {
        public UserCreateException(string mess, int statusCode = 400) : base(mess, statusCode)
        {
        }
    }
}
