using System;
using BuySell.Model.RestResponse;

namespace BuySell.Model.SignUpResponse
{
    public class SignUpResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
