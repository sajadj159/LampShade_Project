using System.Reflection;
using BlogManagement.Application.Features.Articles.Commands.CreateArticle;
using BlogManagement.Application.ReadModel.Article;
using BlogManagement.Application.ReadModel.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore.Repository;
using BlogManagement.Infrastructure.ReadModel;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Infrastructure;

public sealed class BlogModule : IModule
{
    public string Name => "Blog";

    public IEnumerable<Assembly> HandlerAssemblies => [typeof(CreateArticleCommandHandler).Assembly, typeof(BlogModule).Assembly];

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<BlogContext>(configuration, BlogContext.Schema);
        services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<IArticleQuery, ArticleQuery>();
        services.AddTransient<IArticleCategoryQuery, ArticleCategoryQuery>();
    }

    public Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken) =>
        services.GetRequiredService<BlogContext>().Database.MigrateAsync(cancellationToken);
}
