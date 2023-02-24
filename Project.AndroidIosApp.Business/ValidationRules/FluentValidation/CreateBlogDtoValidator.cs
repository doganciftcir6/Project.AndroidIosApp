using FluentValidation;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateBlogDtoValidator : AbstractValidator<CreateBlogDto>
    {
        public CreateBlogDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz!");
            RuleFor(x => x.Title).MaximumLength(500).WithMessage("Başlık alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Title).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Subtitle).NotEmpty().WithMessage("Alt başlık alanı boş olamaz!");
            RuleFor(x => x.Subtitle).MaximumLength(500).WithMessage("Başlık alanı en fazla 500 karakter içerebilir!");
            RuleFor(x => x.Subtitle).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş olamaz!");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Açıklama alanı en fazla 1000 karakter içerebilir!");
            RuleFor(x => x.Description).MinimumLength(6).WithMessage("Açıklama alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Description2).MaximumLength(1000).WithMessage("Açıklama2 alanı en fazla 1000 karakter içerebilir!");
            RuleFor(x => x.Description2).MinimumLength(6).WithMessage("Açıklama2 alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Description3).MaximumLength(1000).WithMessage("Açıklama3 alanı en fazla 1000 karakter içerebilir!");
            RuleFor(x => x.Description3).MinimumLength(6).WithMessage("Açıklama3 alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Description4).MaximumLength(1000).WithMessage("Açıklama3 alanı en fazla 1000 karakter içerebilir!");
            RuleFor(x => x.Description4).MinimumLength(6).WithMessage("Açıklama3 alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Company).NotEmpty().WithMessage("Şirket alanı boş olamaz!");
            RuleFor(x => x.Company).MaximumLength(200).WithMessage("Başlık alanı en fazla 200 karakter içerebilir!");
            RuleFor(x => x.Company).MinimumLength(6).WithMessage("Başlık alanı en az 6 karakter içermelidir!");
            RuleFor(x => x.Image1).NotEmpty().WithMessage("Bir görsel seçiniz!");
        }
    }
}
