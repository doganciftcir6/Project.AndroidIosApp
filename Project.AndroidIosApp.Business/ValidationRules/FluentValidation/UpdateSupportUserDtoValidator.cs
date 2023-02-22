using FluentValidation;
using Project.AndroidIosApp.Dtos.SupportUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class UpdateSupportUserDtoValidator : AbstractValidator<UpdateSupportUserDto>
    {
        public UpdateSupportUserDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.SupportName).NotEmpty().WithMessage("Destek Kullancısı İsim alanı boş olamaz!");
            RuleFor(x => x.SupportName).MaximumLength(50).WithMessage("Destek Kullancısı İsim alanı en fazla 50 karakter içerebilir!");
            RuleFor(x => x.SupportName).MinimumLength(6).WithMessage("Destek Kullancısı İsim alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.SupportLastname).NotEmpty().WithMessage("Destek Kullancısı Soy isim alanı boş olamaz!");
            RuleFor(x => x.SupportLastname).MaximumLength(50).WithMessage("Destek Kullancısı Soy isim alanı en fazla 50 karakter içerebilir!");
            RuleFor(x => x.SupportLastname).MinimumLength(6).WithMessage("Destek Kullancısı Soy isim alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.SupportPhone).NotEmpty().WithMessage("Destek Kullancısı Telefon numarası alanı boş olamaz!");
            RuleFor(x => x.SupportPhone).MaximumLength(11).WithMessage("Destek Kullancısı Telefon numarası alanı en fazla 11 karakter içerebilir!");
            RuleFor(x => x.SupportPhone).MinimumLength(11).WithMessage("Destek Kullancısı Telefon numarası alanı en az 11 karakter içermelidir!");
            RuleFor(x => x.SupportEmail).NotEmpty().WithMessage("Destek Kullancısı Mail alanı boş olamaz!");
            RuleFor(x => x.SupportEmail).EmailAddress().WithMessage("Geçerli bir email adresi girmediniz!");
        }
    }
}
