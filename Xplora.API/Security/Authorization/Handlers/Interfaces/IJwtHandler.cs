using XploraAPI.Security.Domain.Models;

namespace XploraAPI.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}