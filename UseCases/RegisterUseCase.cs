using API_REST.DTOs;
using API_REST.DTOs.Request;
using API_REST.DTOs.Respond;
using API_REST.Infrastructure.Data;
using API_REST.Infrastructure.Models;
using API_REST.Interfaces;
using API_REST.Utils;
using AutoMapper;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace API_REST.UseCases
{
    public class RegisterUseCase : IRegisterInterface
    {
        private readonly IMapper _mapper;
        private readonly MasterContext _masterContext;

        public  RegisterUseCase(IMapper mapper, MasterContext masterContext) 
        {
            _mapper = mapper;
            _masterContext = masterContext;
        }

        public async Task<UserRespondDTO> Registration(UserRequestDTO userRequestDTO)
        {

            PasswordTools.CreatePassword(userRequestDTO.UserPassword, out PasswordDTO passwordDTO);
       
            var user = _mapper.Map<User>(userRequestDTO);
            user.PasswordHash = passwordDTO.PasswordHash;
            user.PasswordSalt = passwordDTO.PasswordSalt;

            _masterContext.Add(user);
            await _masterContext.SaveChangesAsync();

            UserRespondDTO userRespondDTO = new UserRespondDTO();
            userRespondDTO = _mapper.Map<UserRespondDTO>(user);

            return userRespondDTO;
        }

        
    }
}
