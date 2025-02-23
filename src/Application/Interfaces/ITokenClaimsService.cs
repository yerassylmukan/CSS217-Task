namespace Application.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GenerateToken(string username);
}