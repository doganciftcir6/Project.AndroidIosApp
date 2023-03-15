using FluentValidation;
using Project.AndoridIosApp.UI.Areas.Admin.Models;

namespace Project.AndoridIosApp.UI.ValidationRules
{
    public class CreateDeviceModelValidator : AbstractValidator<CreateDeviceModel>
    {
        public CreateDeviceModelValidator()
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
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Görsel alanı boş olamaz!");
        }
    }
}
