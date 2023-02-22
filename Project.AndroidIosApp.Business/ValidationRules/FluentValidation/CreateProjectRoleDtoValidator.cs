using FluentValidation;
using Project.AndroidIosApp.Dtos.ProjectRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateProjectRoleDtoValidator : AbstractValidator<CreateProjectRoleDto>
    {
        public CreateProjectRoleDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama alanı boş olamaz!");
        }
    }
}
