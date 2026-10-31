using LampShade.BuildingBlocks.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<long,Account>
    {
        Task<Account> GetByAsync(string userName, CancellationToken cancellationToken = default);
    }
}
