using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Helpers.UploadImageHelper;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Helpers.DeviceHelper
{
    public class DeviceImageUploadThenAferwwrootHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private static DeviceImageUploadThenAferwwrootHelper _instance;
        public DeviceImageUploadThenAferwwrootHelper(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        //newlemekten kurtul
        public static DeviceImageUploadThenAferwwrootHelper CreateInstance(IHostingEnvironment hostingEnvironment)
        {
            _instance = new DeviceImageUploadThenAferwwrootHelper(hostingEnvironment);
            return _instance;
        }

        public async Task<IDataResponse<string>> RunDeviceUploadAsync(IFormFile file)
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
                //pathtteki wwwroottan sonrasını dbye kaydeden upload burada olacak çünkü ıformfile modelde verdim(controller için geçerliydi helperdan önce).
                var uploadfileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extName = Path.GetExtension(file.FileName);
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "img/device", uploadfileName + DateTime.Now.Ticks.ToString() + extName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                //wwwtrottan sonrasını kayıt edelim dbye
                string wwwrootSonrasi = path.Replace(_hostingEnvironment.WebRootPath, "").Replace('\\', '/');
                //eğer upload başarılı ise wwwrootsonrasi pathi geri yollasınki ben ilgili businees veya controllerda bunu yakalayıp mapleme yapabileyim modelime veya dtoma.
                return new DataResponse<string>(ResponseType.Success, wwwrootSonrasi, "Upload işlemi başarılı...");
            }
            else
            {
                //upload başarılı değilse hata mesajı döndürelim checklerden birinde sorun var.
                return new DataResponse<string>(ResponseType.Error, $"{file.Name}" + " " + "Alanı için " + " " + imageRuleChecks.Meessage);
            }
        }
    }
}
