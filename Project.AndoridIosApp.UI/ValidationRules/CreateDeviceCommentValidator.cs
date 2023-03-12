using FluentValidation;
using Project.AndoridIosApp.UI.Areas.Admin.Models;

namespace Project.AndoridIosApp.UI.ValidationRules
{
    public class CreateDeviceCommentValidator : AbstractValidator<CreateDeviceCommentModel>
    {
        public CreateDeviceCommentValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş olamaz!");
            RuleFor(x => x.Content).MaximumLength(2000).WithMessage("İçerik alanı en fazla 2000 karakter içerebilir!");
            RuleFor(x => x.Content).MinimumLength(6).WithMessage("İçerik alanı en az 6 karakter içermek zorundadır!");
        }
    }
}
