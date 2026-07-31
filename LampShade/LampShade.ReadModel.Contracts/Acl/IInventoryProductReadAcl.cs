namespace LampShade.ReadModel.Contracts.Acl;

public interface IInventoryProductReadAcl
{
    Task<string> GetProductNameAsync(long productId, CancellationToken cancellationToken = default);
}