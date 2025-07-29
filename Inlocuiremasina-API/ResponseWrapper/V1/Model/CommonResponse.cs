using System.Net;

namespace ResponseWrapper.V1.Model
{
    public class CommonResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public bool IsError { get; set; }
    }
}
