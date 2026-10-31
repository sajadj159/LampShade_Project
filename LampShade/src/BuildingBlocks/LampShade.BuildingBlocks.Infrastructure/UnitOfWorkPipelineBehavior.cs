using _0_Framework.Application;
using _0_Framework.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LampShade.Api.MediatR;

public sealed class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IEnumerable<IDbContext> dbContexts)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next();

        if (request is not ICommand<TResponse>)
            return response;

        var changedContexts = dbContexts.Where(x => x.ChangeTracker.HasChanges()).ToList();
        if (changedContexts.Count == 0)
            return response;

        await using var transaction = await changedContexts[0].Database.BeginTransactionAsync(cancellationToken);
        try
        {
            foreach (var dbContext in changedContexts.Skip(1))
                dbContext.Database.UseTransaction(transaction.GetDbTransaction());

            foreach (var dbContext in changedContexts)
                await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        if (response is ICommitAwareResponse commitAwareResponse)
            commitAwareResponse.OnCommitted();

        return response;
    }
}