using FluentValidation;
using Project.AndroidIosApp.Dtos.ContactDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz!");
            RuleFor(x => x.Title).MaximumLength(200).WithMessage("Başlık alanı en fazla 200 karakter içerebilir!");
            RuleFor(x => x.Title).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş olamaz!");
            RuleFor(x => x.Content).MaximumLength(3000).WithMessage("İçerik alanı en fazla 3000 karakter içerebilir!");
            RuleFor(x => x.Content).MinimumLength(6).WithMessage("İçerik alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Adress).NotEmpty().WithMessage("Adres alanı boş olamaz!");
            RuleFor(x => x.Adress).MaximumLength(500).WithMessage("Adres alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Adress).MinimumLength(6).WithMessage("Adres alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Skype).NotEmpty().WithMessage("Skype alanı boş olamaz!");
            RuleFor(x => x.Skype).MaximumLength(100).WithMessage("Skype alanı en fazla 100 karakter içerebilir!");
            RuleFor(x => x.Skype).MinimumLength(6).WithMessage("Skype alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail alanı boş olamaz!");
            RuleFor(x => x.Mail).EmailAddress().WithMessage("Geçerli bir email adresi girmediniz!");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Telefon numarası alanı boş olamaz!");
            RuleFor(x => x.Phone).MaximumLength(11).WithMessage("Telefon numarası alanı en fazla 11 karakter içerebilir!");
            RuleFor(x => x.Phone).MinimumLength(11).WithMessage("Telefon numarası alanı en az 11 karakter içermelidir!");
        }
    }
}
