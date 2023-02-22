using FluentValidation;
using Project.AndroidIosApp.Dtos.GenderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateGenderDtoValidator : AbstractValidator<CreateGenderDto>
    {
        public CreateGenderDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama alanı boş olamaz!");
        }
    }
}
