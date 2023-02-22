using FluentValidation;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.ValidationRules.FluentValidation
{
    public class CreateBlogCommentDtoValidator : AbstractValidator<CreateBlogCommentDto>
    {
        public CreateBlogCommentDtoValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş olamaz!");
            RuleFor(x => x.Content).MaximumLength(2000).WithMessage("İçerik alanı en fazla 2000 karakter içerebilir!");
            RuleFor(x => x.Content).MinimumLength(6).WithMessage("İçerik alanı en az 6 karakter içermek zorundadır!");
        }
    }
}
