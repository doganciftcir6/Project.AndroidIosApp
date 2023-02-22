using FluentValidation;
using Project.AndroidIosApp.Dtos.SocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class UpdateSocialMediaDtoValidator : AbstractValidator<UpdateSocialMediaDto>
    {
        public UpdateSocialMediaDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş olamaz!");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("İsim alanı en fazla 100 karakter olabilir!");
            RuleFor(x => x.Name).MinimumLength(6).WithMessage("İsim alanı en az 6 karakter olmalıdır!");
            RuleFor(x => x.Url).NotEmpty().WithMessage("Url alanı boş olamaz!");
            RuleFor(x => x.Url).MaximumLength(300).WithMessage("Url alanı en fazla 300 karakter olabilir!");
            RuleFor(x => x.Url).MinimumLength(6).WithMessage("Url alanı en az 6 karakter olmalıdır!");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("İkon alanı boş olamaz!");
            RuleFor(x => x.Icon).MaximumLength(300).WithMessage("İkon alanı en fazla 300 karakter olabilir!");
            RuleFor(x => x.Icon).MinimumLength(6).WithMessage("İkon alanı en az 6 karakter olmalıdır!");
        }
    }
}
