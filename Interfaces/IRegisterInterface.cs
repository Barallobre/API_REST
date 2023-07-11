using API_REST.DTOs.Respond;
using API_REST.Infrastructure.Models;

namespace API_REST.Interfaces
{
    public interface IRegisterInterface
    {
        public Task<UserRespondDTO> Registration(User user);
    }
}
