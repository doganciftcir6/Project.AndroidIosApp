using AutoMapper;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Mapping.AutoMapper
{
    public class BlogCommentProfile : Profile
    {
        public BlogCommentProfile()
        {
            CreateMap<BlogComment, CreateBlogCommentDto>().ReverseMap();
            CreateMap<BlogComment, UpdateBlogCommentDto>().ReverseMap();
            CreateMap<BlogComment, GetBlogCommentDto>().ReverseMap();
        }
    }
}
