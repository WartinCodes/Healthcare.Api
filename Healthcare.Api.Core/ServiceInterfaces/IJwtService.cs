namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userName);
    }
}
