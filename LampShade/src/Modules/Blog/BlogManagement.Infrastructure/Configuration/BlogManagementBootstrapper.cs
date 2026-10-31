using LampShade.ReadModel.Contracts.Article;
using LampShade.ReadModel.Contracts.ArticleCategory;
using LampShade.ReadModel.Application.Query;
using BlogManagement.Application.A.Article;
using BlogManagement.Application.Contract.AC.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();

            service.AddTransient<IArticleRepository, ArticleRepository>();
            service.AddTransient<IArticleApplication, ArticleApplication>();

            service.AddTransient<IArticleQuery, ArticleQuery>();
            service.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();
            
            service.AddDbContext<BlogContext>((sp, options) => options.UseNpgsql(sp.GetRequiredService<NpgsqlConnection>()));
            service.AddScoped<_0_Framework.Domain.IDbContext>(sp => sp.GetRequiredService<BlogContext>());
        }
    }
}


