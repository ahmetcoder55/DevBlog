using DevBlog.Business.Abstract.Interfaces;
using DevBlog.Business.Abstract.Interfaces.UnitOfWorks;
using DevBlog.Business.Concrete;
using DevBlog.Business.Concrete.Services;
using DevBlog.DataAccess.Concrete.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.Business.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureBusiness(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDataAccessServices(configuration);
            services.AddScoped<IArticleService, ArticleManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
