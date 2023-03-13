using FluentValidation;
using Project.AndoridIosApp.UI.Areas.Admin.Models;

namespace Project.AndoridIosApp.UI.ValidationRules
{
    public class UpdateMessageModelValidator : AbstractValidator<UpdateMessageModel>
    {
        public UpdateMessageModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz!");
            RuleFor(x => x.Title).MaximumLength(500).WithMessage("Başlık alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Title).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş olamaz!");
            RuleFor(x => x.Content).MaximumLength(1000).WithMessage("İçerik alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Content).MinimumLength(6).WithMessage("İçerik alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Receiver).NotEmpty().WithMessage("Receiver alanı boş olamaz!");
            RuleFor(x => x.Receiver).MaximumLength(500).WithMessage("Receiver alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Receiver).MinimumLength(6).WithMessage("Receiver alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.ReceiverName).NotEmpty().WithMessage("ReceiverName alanı boş olamaz!");
            RuleFor(x => x.ReceiverName).MaximumLength(500).WithMessage("ReceiverName alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.ReceiverName).MinimumLength(6).WithMessage("ReceiverName alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Sender).NotEmpty().WithMessage("Sender alanı boş olamaz!");
            RuleFor(x => x.Sender).MaximumLength(500).WithMessage("Sender alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Sender).MinimumLength(6).WithMessage("Sender alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.SenderName).NotEmpty().WithMessage("SenderName alanı boş olamaz!");
            RuleFor(x => x.SenderName).MaximumLength(500).WithMessage("SenderName alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.SenderName).MinimumLength(6).WithMessage("SenderName alanı en az 6 karakter içermelidir!");

            RuleFor(x => x.DeviceId).NotEmpty().WithMessage("DeviceId alanı boş olamaz!");
        }
    }
}
