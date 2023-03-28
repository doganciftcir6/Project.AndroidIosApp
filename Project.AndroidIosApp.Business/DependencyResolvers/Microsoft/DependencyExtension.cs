using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Business.Concrete.Managers;
using Project.AndroidIosApp.Business.ValidationRules.FluentValidation;
using Project.AndroidIosApp.DataAccess.Abstract.Repositories;
using Project.AndroidIosApp.DataAccess.Concrete.Repositories;
using Project.AndroidIosApp.DataAccess.Contexts.EntityFramework;
using Project.AndroidIosApp.DataAccess.UnitOfWork;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.ContactDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using Project.AndroidIosApp.Dtos.GenderDto;
using Project.AndroidIosApp.Dtos.OSDtos;
using Project.AndroidIosApp.Dtos.ProjectRole;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Dtos.SocialMediaDtos;
using Project.AndroidIosApp.Dtos.SupportDtos;

namespace Project.AndroidIosApp.Business.DependencyResolvers.Microsoft
{
    //startup genişletmek amaç. Microsoft.Extension.DependencyInjection.Abstraction paketi gerek. Microsoft.Extensions.Configuration gerek. EntityFrameworkCore gerek.
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //sql
            services.AddDbContext<AndroidIosContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });

            //scopes
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IContactService,ContactManager>();
            services.AddScoped<IDeviceService, DeviceManager>();
            services.AddScoped<IGenderService, GenderManager>();
            services.AddScoped<IProjectRoleService, ProjectRoleManager>();
            services.AddScoped<IProjectUserService, ProjectUserManager>();
            services.AddScoped<IProjectUserRoleService, ProjectUserRoleManager>();
            services.AddScoped<ISocialMediaService, SocialMediaManager>();
            services.AddScoped<ISupportService, SupportManager>();


            services.AddScoped<IOSService, OSManager>();
            services.AddScoped<IDeviceTypeService, DeviceTypeManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IBlogCommentService, BlogCommentManager>();

            services.AddScoped<IDeviceDal, EfDeviceDal>();
            services.AddScoped<ICommentDal, EfCommentDal>();

            services.AddScoped<IBlogService, BlogManager>();

            //FluentValidation
            services.AddTransient<IValidator<CreateBlogDto>,CreateBlogDtoValidator>();
            services.AddTransient<IValidator<UpdateBlogDto>,UpdateBlogDtoValidator>();
            services.AddTransient<IValidator<CreateContactDto>,CreateContactDtoValidator>();
            services.AddTransient<IValidator<UpdateContactDto>,UpdateContactDtoValidator>();
            services.AddTransient<IValidator<CreateDeviceDto>,CreateDeviceDtoValidator>();
            services.AddTransient<IValidator<UpdateDeviceDto>, UpdateDeviceDtoValidator>();
            services.AddTransient<IValidator<CreateGenderDto>,CreateGenderDtoValidator>();
            services.AddTransient<IValidator<UpdateGenderDto>,UpdateGenderDtoValidator>();
            services.AddTransient<IValidator<CreateProjectRoleDto>,CreateProjectRoleDtoValidator>();
            services.AddTransient<IValidator<UpdateProjectRoleDto>,UpdateProjectRoleDtoValidator>();
            services.AddTransient<IValidator<CreateProjectUserDto>,CreateProjectUserDtoValidator>();
            services.AddTransient<IValidator<UpdateProjectUserDto>,UpdateProjectUserDtoValidator>();
            services.AddTransient<IValidator<CreateSocialMediaDto>,CreateSocialMediaDtoValidator>();
            services.AddTransient<IValidator<UpdateSocialMediaDto>,UpdateSocialMediaDtoValidator>();
            services.AddTransient<IValidator<CreateSupportDto>,CreateSupportDtoValidator>();
            services.AddTransient<IValidator<UpdateSupportDto>,UpdateSupportDtoValidator>();
            services.AddTransient<IValidator<LoginProjectUserDto>,LoginProjectUserDtoValidator>();

            services.AddTransient<IValidator<CreateOSDto>,CreateOSDtoValidator>();
            services.AddTransient<IValidator<UpdateOSDto>,UpdateOSDtoValidator>();
            services.AddTransient<IValidator<CreateDeviceTypeDto>,CreateDeviceTypeDtoValidator>();
            services.AddTransient<IValidator<UpdateDeviceTypeDto>,UpdateDeviceTypeDtoValidator>();

            services.AddTransient<IValidator<CreateCommentDto>,CreateCommentDtoValidator>();
            services.AddTransient<IValidator<UpdateCommentDto>,UpdateCommentDtoValidator>();
            services.AddTransient<IValidator<CreateBlogCommentDto>,CreateBlogCommentDtoValidator>();
            services.AddTransient<IValidator<UpdateBlogCommentDto>,UpdateBlogCommentDtoValidator>();
        }
    }
}
