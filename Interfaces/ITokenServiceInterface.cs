using API_REST.Infrastructure.Models;

namespace API_REST.Interfaces
{
    public interface ITokenServiceInterface
    {
        public string CreateToken(User user);
    }
}
