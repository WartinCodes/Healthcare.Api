namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IAuthService
    {
        string EncryptPassword(string password);
    }
}
