using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Helpers.UploadImageHelper;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.BlogDtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Helpers
{
    //Bu helper'a static yapamam newlemek zorundayım çünkü dependency olarak IHostingEnvironment kullanmak istiyorum. Tabi bu kullanılmadan da static şekilde upload yapılabilir başka yöntemler ile. Tercih meselesi.
    public class BlogImageUploadAfterWwwroot
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public BlogImageUploadAfterWwwroot(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IDataResponse<string>> RunUpload(IFormFile file)
        {
            var imageRuleChecks = ImageUploadCheckHelper.Run
                   (
                       ImageUploadRuleHelper.CheckImageName(file.FileName),
                       ImageUploadRuleHelper.CheckIfImageExtensionsAllow(file.FileName),
                       ImageUploadRuleHelper.CheckIfImageSizeIsLessThanOneMb(file.FileName.Length)
                   );
            if (imageRuleChecks.ResponseType == ResponseType.Success)
            {
                //pathtteki wwwroottan sonrasını dbye kaydeden upload burada olacak çünkü ıformfile modelde verdim.
                var uploadfileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extName = Path.GetExtension(file.FileName);
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "img/blog", uploadfileName + DateTime.Now.Ticks.ToString() + extName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                //wwwtrottan sonrasını kayıt edelim dbye
                string wwwrootSonrasi = path.Replace(_hostingEnvironment.WebRootPath, "").Replace('\\', '/');
                //eğer upload başarılı ise wwwrootsonrasi pathi geri yollasınki ben ilgili businees veya controllerda bunu yakalayıp mapleme yapabileyim modelime veya dtoma.
                return new DataResponse<string>(ResponseType.Success, wwwrootSonrasi);
            }
            else
            {
                //upload başarılı değilse hata mesajı döndürelim checklerden birinde sorun var.
                return new DataResponse<string>(ResponseType.Error, $"{file.Name}" + " " + "Alanı için " + " " + imageRuleChecks.Meessage);
            }
        }
    }
}
