using System;
namespace BuySell.Model.RestResponse
{
    public class RestResponseModel
    {
        public string content_type { get; set; }
        public long content_length { get; set; }
        public int status_code { get; set; }
        public string response_body { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ResponseBodyModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
       
    }
}
