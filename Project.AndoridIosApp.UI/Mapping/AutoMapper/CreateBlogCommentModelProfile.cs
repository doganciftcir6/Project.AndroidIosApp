using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using Project.AndroidIosApp.Dtos.CommentDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class CreateBlogCommentModelProfile : Profile
    {
        public CreateBlogCommentModelProfile()
        {
            CreateMap<CreateBlogCommentModel, CreateBlogCommentDto>().ReverseMap();
        }
    }
}
