using System;
namespace BuySell.Model.RestResponse
{
    public class ErrorResponseModel
    {
        public string message { get; set; }
        public bool isError { get; set; }
        public object detail { get; set; }
        public object errors { get; set; }
    }
}
