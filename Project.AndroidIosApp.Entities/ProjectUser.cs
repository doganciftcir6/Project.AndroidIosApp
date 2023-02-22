using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class ProjectUser : BaseEntity
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string PasswordVerify { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        //gender ilişkisi(çok-bir, bir user'ın bir gender'ı)
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        //role ilişkisi (çoka-çok)
        public List<ProjectUserRole> ProjectUserRoles { get; set; }
        //Support ilişkisi (bire-çok, bir userın birden çok supportu)
        public List<Support> Supports { get; set; }
        //Comment İlişkisi bir userın birden çok commenti
        public List<Comment> Comments { get; set; }
        //BlogComment İlişkisi bir userın birden çok BlogCommenti
        public List<BlogComment> BlogComments { get; set; }

    }
}
