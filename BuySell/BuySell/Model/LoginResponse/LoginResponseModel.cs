using System;
namespace BuySell.Model.LoginResponse
{
    public class Data
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public object ProfilePath { get; set; }
        public bool RequirePasswordChange { get; set; }
        public string PGCustomerId { get; set; }
    }

    public class LoginResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }

    public class ResetPasswordResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
