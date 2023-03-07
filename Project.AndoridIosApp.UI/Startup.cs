using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Project.AndoridIosApp.UI.Mapping.AutoMapper;
using Project.AndoridIosApp.UI.Models;
using Project.AndoridIosApp.UI.ValidationRules;
using Project.AndroidIosApp.Business.DependencyResolvers.Microsoft;
using Project.AndroidIosApp.Business.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI
{
    public class Startup
    {
        //IConfiguration'u businessta extension'a gönderebilmek için.(sql baðlamak için gerekli bu sýnýf)
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //DependencyExtension unu kullanabilmek.
            services.AddDependencies(Configuration);

            //modelin FluenValidationu
            services.AddTransient <IValidator<UserCreateModel>, UserCreateModelValidator>();
            services.AddTransient<IValidator<SendMessageModel>, SendMessageModelValidator>();  
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //login cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.Cookie.Name = "IosAndroidCookie";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SameSite = SameSiteMode.Strict;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.ExpireTimeSpan = TimeSpan.FromHours(1);
                opt.LoginPath = new PathString("/Auth/SignIn");
                opt.LogoutPath = new PathString("/Auth/LogOut");
                opt.AccessDeniedPath = new PathString("/Auth/AccessDenied");
            });

            //mvc
            services.AddControllersWithViews();

            //automapper
            var profiles = MapperHelper.GetProfiles();
            profiles.Add(new UserCreateModelProfile());
            profiles.Add(new SendMessageModelProfile());
            var configurations = new MapperConfiguration(opt =>
            {
                opt.AddProfiles(profiles);
            });
            var mapper = configurations.CreateMapper();
            services.AddSingleton(mapper);
        }

     

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //error site
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/");

            app.UseStaticFiles();

            app.UseRouting();

            //Auth iþlemleri
            app.UseAuthentication();
            app.UseAuthorization();

            //auto route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
