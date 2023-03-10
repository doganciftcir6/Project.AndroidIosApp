using FluentValidation;
using Project.AndoridIosApp.UI.Areas.Admin.Models;

namespace Project.AndoridIosApp.UI.ValidationRules
{
    public class UpdateProjectUserModelValidator : AbstractValidator<UpdateProjectUserModel>
    {
        public UpdateProjectUserModelValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id alanı boş olamaz!");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz!");
            RuleFor(x => x.Username).MaximumLength(50).WithMessage("Kullanıcı adı alanı en fazla 50 karakter içerebilir!");
            RuleFor(x => x.Username).MinimumLength(6).WithMessage("Kullanıcı adı alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("İsim alanı boş olamaz!");
            RuleFor(x => x.Firstname).MaximumLength(50).WithMessage("İsim alanı en fazla 50 karakter içerebilir!");
            RuleFor(x => x.Firstname).MinimumLength(6).WithMessage("İsim alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("Soy isim alanı boş olamaz!");
            RuleFor(x => x.Lastname).MaximumLength(50).WithMessage("Soy isim alanı en fazla 50 karakter içerebilir!");
            RuleFor(x => x.Lastname).MinimumLength(6).WithMessage("Soy isim alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz!");
            RuleFor(x => x.Password).Matches(x => x.PasswordVerify).WithMessage("Şifre alanı Şifre tekrarı alanı ile eşleşmiyor!");
            RuleFor(x => x.PasswordVerify).NotEmpty().WithMessage("Şifre Tekrarı alanı boş olamaz!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası alanı boş olamaz!");
            RuleFor(x => x.PhoneNumber).MaximumLength(11).WithMessage("Telefon numarası alanı en fazla 11 karakter içerebilir!");
            RuleFor(x => x.PhoneNumber).MinimumLength(11).WithMessage("Telefon numarası alanı en az 11 karakter içermelidir!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail alanı boş olamaz!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir email adresi girmediniz!");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Cinsiyet alanı boş olamaz!");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Cinsiyet seçimi zorunludur.");
        }
    }
}
