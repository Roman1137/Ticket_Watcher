using System.Net;

namespace TrainTicketWatcher
{
    public class CustomResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseContent { get; set; }
        public override string ToString()
        {
            return $"Status code: {this.StatusCode}\r\nContent: {this.ResponseContent}";
        }
    }
}
