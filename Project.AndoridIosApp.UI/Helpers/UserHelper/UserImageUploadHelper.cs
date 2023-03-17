using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Helpers.UploadImageHelper;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using System.IO;
using System.Threading.Tasks;
using System;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Dtos.Interfaces;

namespace Project.AndoridIosApp.UI.Helpers.UserHelper
{
    public class UserImageUploadHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private static UserImageUploadHelper _instance;
        public UserImageUploadHelper(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        //newlemekten kurtul
        public static UserImageUploadHelper CreateInstance(IHostingEnvironment hostingEnvironment)
        {
            _instance = new UserImageUploadHelper(hostingEnvironment);
            return _instance;
        }

        public async Task<IDataResponse<string>> RunUploadAsync(IFormFile file)
        {
            var imageRuleChecks = ImageUploadCheckHelper.Run
                   (
                       ImageUploadRuleHelper.CheckImageName(file.FileName),
                       ImageUploadRuleHelper.CheckIfImageExtensionsAllow(file.FileName),
                       ImageUploadRuleHelper.CheckIfImageSizeIsLessThanOneMb(file.Length),
                       ImageUploadRuleHelper.CheckImageNameDot(file)
                   );
            if (imageRuleChecks.ResponseType == ResponseType.Success)
            {
                //upload burada olacak çünkü ıformfile modelde verdim.(controllerdayken geçerliydi halperdan önce.)
                var fileName = Guid.NewGuid().ToString();
                var extName = Path.GetExtension(file.FileName);
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "userImage", fileName + extName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                var dbCreateFileName = fileName + extName;
                return new DataResponse<string>(ResponseType.Success, dbCreateFileName);
            }
            else
            {
                //upload başarılı değilse hata mesajı döndürelim checklerden birinde sorun var.
                return new DataResponse<string>(ResponseType.Error, $"{file.Name}" + " " + "Alanı için " + " " + imageRuleChecks.Meessage);
            }
        }
    }
}
