using System.Reflection;
using CommentManagement.Application.Features.Comments.Commands.AddComment;
using CommentManagement.Application.ReadModel.Comment;
using CommentManagement.Contracts;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using CommentManagement.Infrastructure.ReadModel;
using LampShade.BuildingBlocks.Infrastructure.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Infrastructure;

public sealed class CommentModule : IModule
{
    public string Name => "Comment";

    public IEnumerable<Assembly> HandlerAssemblies => [typeof(AddCommentCommandHandler).Assembly, typeof(CommentModule).Assembly];

    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddModuleDbContext<CommentContext>(configuration, CommentContext.Schema);
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<ICommentQuery, CommentQuery>();
        services.AddTransient<ICommentModuleApi, CommentModuleApi>();
    }

    public Task InitializeAsync(IServiceProvider services, CancellationToken cancellationToken) =>
        services.GetRequiredService<CommentContext>().Database.MigrateAsync(cancellationToken);
}
