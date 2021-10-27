namespace Utm.Application.Repositories.Rest
{
    public interface IRestRepository
    {
        string PostJsonRequest(string body, string url, string resource);
        string GetJsonRequest(string url, string resource);
    }
}