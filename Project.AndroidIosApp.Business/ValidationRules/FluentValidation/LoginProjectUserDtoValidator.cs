using FluentValidation;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class LoginProjectUserDtoValidator : AbstractValidator<LoginProjectUserDto>
    {
        public LoginProjectUserDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz!");
        }
    }
}
