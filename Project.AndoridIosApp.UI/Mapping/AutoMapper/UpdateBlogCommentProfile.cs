using AutoMapper;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;

namespace Project.AndoridIosApp.UI.Mapping.AutoMapper
{
    public class UpdateBlogCommentProfile : Profile
    {
        public UpdateBlogCommentProfile()
        {
            CreateMap<UpdateBlogCommentModel, UpdateBlogCommentDto>().ReverseMap();
        }
    }
}
