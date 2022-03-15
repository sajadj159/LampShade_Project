using _01_LampShadeQuery.Contract.Comment;
using _01_LampShadeQuery.Query;
using CommentManagement.Application.Comment;
using CommentManagement.Application.Contract.A.Comment;
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
        {
            service.AddTransient<ICommentApplication, CommentApplication>();
            service.AddTransient<ICommentRepository, CommentRepository>();

            service.AddTransient<ICommentQuery, CommentQuery>();

            service.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionString));
        }
    }
}