using LampShade.BuildingBlocks.Application;
using MediatR;

namespace LampShade.BuildingBlocks.Infrastructure;

/// <summary>
/// Commits a command's changes. A command belongs to exactly one module, so it may only change
/// that module's DbContext; anything another module must do happens through an integration event.
/// </summary>
public sealed class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IEnumerable<IDbContext> dbContexts)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var response = await next(cancellationToken);

        if (request is not ICommand<TResponse>)
            return response;

        var changedContexts = dbContexts.Where(x => x.ChangeTracker.HasChanges()).ToList();
        if (changedContexts.Count == 0)
            return response;

        if (changedContexts.Count > 1)
            throw new InvalidOperationException(
                $"{typeof(TRequest).Name} changed data of more than one module ({string.Join(", ", changedContexts.Select(x => x.GetType().Name))}). " +
                "Cross-module side effects must be published as integration events.");

        await changedContexts[0].SaveChangesAsync(cancellationToken);

        if (response is ICommitAwareResponse commitAwareResponse)
            commitAwareResponse.OnCommitted();

        return response;
    }
}
