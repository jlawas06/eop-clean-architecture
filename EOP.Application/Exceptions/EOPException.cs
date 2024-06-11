using System.Net;

namespace EOP.Application.Exceptions
{
    public class EOPException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public EOPException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public EOPException(string message) : base(message)
        {
        }
    }
}
