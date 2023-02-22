using FluentValidation;
using Project.AndroidIosApp.Dtos.ProjectRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class UpdateProjectRoleDtoValidator : AbstractValidator<UpdateProjectRoleDto>
    {
        public UpdateProjectRoleDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama alanı boş olamaz!");
        }
    }
}
