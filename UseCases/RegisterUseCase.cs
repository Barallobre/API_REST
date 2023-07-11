using API_REST.DTOs;
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
        public async Task<UserRespondDTO> Registration(User user)
        {
            UserRespondDTO userRespondDTO = new UserRespondDTO();

            PasswordTools.CreatePassword(user.Password, out PasswordDTO passwordDTO);
       
            user.Password = string.Format("{0}.{1}", 
                Convert.ToBase64String(passwordDTO.PasswordSalt), Convert.ToBase64String(passwordDTO.PasswordHash));

            _masterContext.Add(user);
            await _masterContext.SaveChangesAsync();
            userRespondDTO = _mapper.Map<UserRespondDTO>(user);

            return userRespondDTO;
        }

        
    }
}
