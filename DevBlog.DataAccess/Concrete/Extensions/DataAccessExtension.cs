using DevBlog.Core.DataAccess.Abstract;
using DevBlog.Core.Entities.Concrete;
using DevBlog.DataAccess.Concrete.Contexts;
using DevBlog.DataAccess.Concrete.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.DataAccess.Concrete.Extensions
{
    public static class DataAccessExtension
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArticleDal, EfArticleDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ITagDal, EfTagDal>();
            services.AddScoped<ICommentDal, EfCommentDal>();

           

            return services;
        }
    }
}
