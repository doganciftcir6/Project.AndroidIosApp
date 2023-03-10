using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using System.Collections.Generic;
using System;

namespace Project.AndoridIosApp.UI.Helpers
{
    public static class UserCreateUploadCheckHelper
    {
        public static IResponse CheckIfImageExtensionsAllow(string fileName)
        {
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> allowFileExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png" };

            if (!allowFileExtensions.Contains(extension))
            {
                return new Response(ResponseType.Error, "Yüklemiş olduğunuz resim .jpg, .jpeg, .gif, .png türlerinden birisi olmalıdır! ");
            }
            return new Response(ResponseType.Success);
        }
        public static IResponse CheckIfImageSizeIsLessThanOneMb(long imageSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imageSize * 0.000001);
            if (imgMbSize > 1)
            {
                return new Response(ResponseType.Error, "Yüklediğiniz resim boyutu 1 mb'dan düşük olmalıdır!");
            }
            return new Response(ResponseType.Success);
        }
        public static IResponse CheckImageName(string fileName)
        {
            if (fileName.Contains("/") || fileName.Contains("<") || fileName.Contains(">") || fileName.Contains("%2F") || fileName.Contains("%5C"))
            {
                return new Response(ResponseType.Error, "Yüklemiş olduğunuz resim ismi /, <, >, %2F, %5C karakterlerini içeremez!");
            }
            return new Response(ResponseType.Success);
        }
    }
}
