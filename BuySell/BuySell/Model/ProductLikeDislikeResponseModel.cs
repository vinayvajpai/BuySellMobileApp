namespace BuySell.Model
{
    internal class ProductLikeRequestModel
    {
        public int ProductId { get; set; }
        public int Status { get; set; }
    }
    public class ProductLikeDislikeResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }
}