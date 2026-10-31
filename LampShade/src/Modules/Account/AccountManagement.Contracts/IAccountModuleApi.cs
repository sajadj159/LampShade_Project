namespace AccountManagement.Contracts;

/// <summary>The Account module's public read API for other modules.</summary>
public interface IAccountModuleApi
{
    /// <summary>Basic account details by id; unknown ids are left out.</summary>
    Task<IReadOnlyDictionary<long, AccountSummary>> GetAccountsAsync(IEnumerable<long> accountIds, CancellationToken cancellationToken = default);
}

public sealed record AccountSummary(long Id, string FullName, string Mobile);
