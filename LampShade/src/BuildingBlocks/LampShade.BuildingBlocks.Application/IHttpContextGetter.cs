namespace LampShade.BuildingBlocks.Application
{
    public interface IHttpContextGetter
    {
        string GetCurrentClaimType(string claimType);
        bool IsAuthenticated();
    }
}
