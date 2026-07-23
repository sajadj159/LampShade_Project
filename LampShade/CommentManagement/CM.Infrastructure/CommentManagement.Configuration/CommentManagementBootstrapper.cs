using LampShade.ReadModel.Contracts.Comment;
using LampShade.ReadModel.Application.Query;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {            service.AddTransient<ICommentRepository, CommentRepository>();

            service.AddTransient<ICommentQuery, CommentQuery>();

            service.AddDbContext<CommentContext>(x => x.UseNpgsql(connectionString));
        }
    }
}


