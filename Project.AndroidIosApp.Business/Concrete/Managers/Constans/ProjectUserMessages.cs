using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Concrete.Managers.Constans
{
    public class ProjectUserMessages
    {
        public static string DeletedProjectUser = "ProjectUser parametresi başarıyla silindi.";
        public static string NotDeletedProjectUser = "ProjectUser parametresi silinemedi çünkü ProjectUser bulanamadı.";
        public static string NotFoundProjectUser = "ProjectUser bulanamadı.";
        public static string NotFoundIdProjectUser = "Id'sine sahip ProjectUser bulanamadı.";
        public static string NotFoundUserNameProjectUser = "UserName'ine sahip ProjectUser bulanamadı.";
        public static string NotFoundEmailProjectUser = "Email'ine sahip ProjectUser bulanamadı.";
        public static string WrongUsernameOrPassword = "Girmiş olduğunuz Kullanıcı adı veya şifre hatalı.";
        public static string NotNullUsernameOrPassword = "Kullanıcı adı ve şifre alanı boş olamaz.";
        public static string NotFoundRole = "İlgili role bulunamadı.";
        public static string SuccessRegister = "Kullanıcı kaydı başarıyla yapıldı.";
        public static string RepeatUsername = "Bu username daha önce kullanılmış!";
        public static string RepeatEmail = "Bu email daha önce kullanılmış!";
    }
}
