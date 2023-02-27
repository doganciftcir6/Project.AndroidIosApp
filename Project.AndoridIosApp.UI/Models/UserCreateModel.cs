using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.AndoridIosApp.UI.Models
{
    public class UserCreateModel
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string PasswordVerify { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IFormFile ImageUrl { get; set; }
        public int GenderId { get; set; }
        public SelectList Genders { get; set; }
    }
}
