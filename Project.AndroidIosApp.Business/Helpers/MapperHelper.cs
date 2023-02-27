using AutoMapper;
using Project.AndroidIosApp.Business.Mapping.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Business.Helpers
{
    public static class MapperHelper
    {
        public static List<Profile> GetProfiles()
        {
            return new List<Profile>()
            {
                //AutoMapper bağlantı dtos  
                new BlogProfile(),
                new ContactBlogProfile(),
                new DeviceProfile(),
                new GenderProfile(),
                new ProjectRoleProfile(),
                new ProjectUserProfile(),
                new ProjectUserRoleProfile(),
                new SocialMediaProfile(),
                new SupportProfile(),
                new OSProfile(),
                new DeviceTypeProfile(),
                new CommentProfile(),
                new BlogCommentProfile(),
            };
        }
    }
}
