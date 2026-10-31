namespace DiscountManagement.Contracts;

/// <summary>The Discount module's public read API for other modules.</summary>
public interface IDiscountModuleApi
{
    /// <summary>Customer discounts that are running right now.</summary>
    Task<List<ProductDiscount>> GetActiveCustomerDiscountsAsync(CancellationToken cancellationToken = default);

    /// <summary>Discounts for colleague (wholesale) accounts that have not been removed.</summary>
    Task<List<ProductDiscount>> GetColleagueDiscountsAsync(CancellationToken cancellationToken = default);
}

public sealed record ProductDiscount(long ProductId, int DiscountRate, DateTime EndDate);
