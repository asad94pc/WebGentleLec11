namespace Lec11.Service
{
    public interface IUserIdentityServices
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}