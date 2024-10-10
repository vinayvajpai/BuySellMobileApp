using System;
using BuySell.Helper;
using Xamarin.Forms;

namespace BuySell.Model.SignUpResponse
{
    public class ProfilePicture
    {
        public string Image { get; set; }
        public string Extension { get; set; }
        public string ImagePath { get; set; }
    }

    public class SignUpRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public ProfilePicture ProfilePicture { get; set; }
    }
}
