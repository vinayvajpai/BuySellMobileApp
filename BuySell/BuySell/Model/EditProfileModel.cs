using System;
using BuySell.ViewModel;

namespace BuySell.Model
{
    //EditProfileModel
    public class EditProfilePicture
    {
        public string Image { get; set; }
        public string Extension { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EditProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public EditProfilePicture ProfilePicture { get; set; }
    }
}
