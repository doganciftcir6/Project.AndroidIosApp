using FluentValidation;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateDeviceTypeDtoValidator : AbstractValidator<CreateDeviceTypeDto>
    {
        public CreateDeviceTypeDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Açıklama alanı boş olamaz!");
            RuleFor(x => x.Definition).MaximumLength(50).WithMessage("Açıklama alanı en fazla 50 karakter içerebilir!!");
            RuleFor(x => x.Definition).MinimumLength(3).WithMessage("Açıklama alanı en az 3 karakter içermelidir!");
        }
    }
}
