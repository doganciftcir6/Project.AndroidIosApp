using FluentValidation;
using Project.AndroidIosApp.Dtos.GenderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class UpdateGenderDtoValidator : AbstractValidator<UpdateGenderDto>
    {
        public UpdateGenderDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama alanı boş olamaz!");
        }
    }
}
