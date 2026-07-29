namespace LampShade.ReadModel.Contracts.Acl;

public sealed record InventoryReadModel(long ProductId, bool InStock, double UnitPrice, long CurrentCount);
public sealed record DiscountReadModel(long ProductId, int DiscountRate, DateTime EndDate);
public sealed record CommentReadModel(long Id, string Name, string Description, int Rating, DateTime CreationDate);

public interface IShopInventoryReadAcl
{
    Task<List<InventoryReadModel>> GetInventoriesAsync(CancellationToken cancellationToken = default);
}

public interface IShopDiscountReadAcl
{
    Task<List<DiscountReadModel>> GetCustomerDiscountsAsync(bool onlyActive, CancellationToken cancellationToken = default);
    List<DiscountReadModel> GetCustomerDiscounts(bool onlyActive);
    List<DiscountReadModel> GetColleagueDiscounts();
}

public interface IShopCommentReadAcl
{
    Task<List<CommentReadModel>> GetConfirmedProductCommentsAsync(long productId, CancellationToken cancellationToken = default);
}