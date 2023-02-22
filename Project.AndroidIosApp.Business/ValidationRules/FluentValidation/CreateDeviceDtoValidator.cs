using FluentValidation;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateDeviceDtoValidator : AbstractValidator<CreateDeviceDto>
    {
        public CreateDeviceDtoValidator()
        {
            RuleFor(x => x.DeviceName).NotEmpty().WithMessage("Cihaz adı alanı boş olamaz!");
            RuleFor(x => x.DeviceName).MaximumLength(100).WithMessage("Cihaz adı alanı en fazla 100 karakter içerebilir!!");
            RuleFor(x => x.DeviceName).MinimumLength(6).WithMessage("Cihaz adı alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.CPU).NotEmpty().WithMessage("CPU alanı boş olamaz!");
            RuleFor(x => x.GPU).NotEmpty().WithMessage("GPU alanı boş olamaz!");
            RuleFor(x => x.MEM).NotEmpty().WithMessage("MEM alanı boş olamaz!");
            RuleFor(x => x.UX).NotEmpty().WithMessage("UX alanı boş olamaz!");
            RuleFor(x => x.TotalScore).NotEmpty().WithMessage("Toplam Skor alanı boş olamaz!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat adı alanı boş olamaz!");
            RuleFor(x => x.ReleaseYear).NotEmpty().WithMessage("Çıkış Yılı alanı boş olamaz!");
            RuleFor(x => x.DeviceName).MinimumLength(8).WithMessage("Çıkış Yılı  alanı en az 8 karakter içermelidir!");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel alanı boş olamaz!");
        }
    }
}
