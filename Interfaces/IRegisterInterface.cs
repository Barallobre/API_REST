using API_REST.DTOs.Request;
using API_REST.DTOs.Respond;

namespace API_REST.Interfaces
{
    public interface IRegisterInterface
    {
        public Task<UserRespondDTO> Registration(UserRequestDTO userRequestDTO);
    }
}
