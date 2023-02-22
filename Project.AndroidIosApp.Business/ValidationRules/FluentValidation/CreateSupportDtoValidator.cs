using FluentValidation;
using Project.AndroidIosApp.Dtos.SupportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateSupportDtoValidator : AbstractValidator<CreateSupportDto>
    {
        public CreateSupportDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz!");
            RuleFor(x => x.Title).MaximumLength(500).WithMessage("Başlık alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Title).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Açıklama alanı boş olamaz!");
            RuleFor(x => x.Content).MaximumLength(500).WithMessage("Başlık alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Content).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.ProjectUserId).NotEmpty().WithMessage("ProjectUserId alanı boş olamaz!");
            RuleFor(x => x.DeviceId).NotEmpty().WithMessage("DeviceId alanı boş olamaz!");
        }
    }
}
