using FluentValidation;
using Project.AndoridIosApp.UI.Models;

namespace Project.AndoridIosApp.UI.ValidationRules
{
    public class SendMessageModelValidator : AbstractValidator<SendMessageModel>
    {
        public SendMessageModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz!");
            RuleFor(x => x.Title).MaximumLength(500).WithMessage("Başlık alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Title).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş olamaz!");
            RuleFor(x => x.Content).MaximumLength(1000).WithMessage("İçerik alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Content).MinimumLength(6).WithMessage("İçerik alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Receiver).NotEmpty().WithMessage("Receiver alanı boş olamaz!");
            RuleFor(x => x.Receiver).MaximumLength(500).WithMessage("Receiver alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Receiver).MinimumLength(6).WithMessage("Receiver alanı en az 6 karakter içermelidir!");

            RuleFor(x => x.DeviceId).NotEmpty().WithMessage("DeviceId alanı boş olamaz!");
        }
    }
}
