using backend.Models;

namespace backend.Interfaces.IServices;

public interface ITokenService
{
    string CreateToken(AppUser user);
}