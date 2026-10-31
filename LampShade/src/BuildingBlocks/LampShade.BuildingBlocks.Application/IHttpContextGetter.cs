namespace _0_Framework.Application
{
    public interface IHttpContextGetter
    {
        string GetCurrentClaimType(string claimType);
        bool IsAuthenticated();
    }
}
