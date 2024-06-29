using M10Backend.Exceptions.Base;

namespace M10Backend.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string mess = "Not found!", int statusCode = 404) : base(mess, statusCode)
        {
            
        }
    }
}
