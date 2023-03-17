using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Project.AndroidIosApp.Core.Helpers.UploadImageHelper
{
    public class ImageUploadCheckHelper
    {
        public static IResponse Run(params IResponse[] logics)
        {
            var errorMessages = logics.Where(x => x.ResponseType != ResponseType.Success)
                                      .Select(x => x.Meessage)
                                      .ToList();

            if (errorMessages.Any())
            {
                // Hata mesajlarını birleştirerek ErrorResponse döndür
                var errorMessage = string.Join("", errorMessages);
                return new Response(ResponseType.Error, errorMessage);
            }
            return new Response(ResponseType.Success);
        }
    }
}
