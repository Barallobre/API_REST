using API_REST.DTOs.Request;
using API_REST.DTOs.Respond;
using API_REST.Exceptions;
using API_REST.Infrastructure.Data;
using API_REST.Infrastructure.Models;
using API_REST.Interfaces;
using API_REST.Services;
using API_REST.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {   
        private readonly IRegisterInterface _register;
        private readonly MasterContext _masterContext;
        private readonly IMapper _mapper;
        private readonly ITokenServiceInterface _tokenService;
        public ApiController(IRegisterInterface register, MasterContext masterContext, IMapper mapper, ITokenServiceInterface tokenService) 
        { 
            _register = register;
            _masterContext = masterContext;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRespondDTO>> RegisterNewUser(UserRequestDTO userRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new MissedDataException("Some neccesary data for registration is missing");
            }
            try
            {
                
                var userRespondDTO = await _register.Registration(userRequestDTO).ConfigureAwait(true);

                return userRespondDTO;
            }
            catch (UserNotValidException ex)
            {
                throw new UserNotValidException("This name user is already taken");
            }
            catch (Exception ex) 
            {
                throw new MissedDataException("Some neccesary data for registration is missing", ex);
            }
            
        }

        [HttpGet("user")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            User user = new User();

            user = await _masterContext.Users.FirstOrDefaultAsync(x => x.Id == userId).ConfigureAwait(true);

            return user;
        }

        [HttpGet("users")]
        public List<UserRespondDTO> GetAllUsers()
        {
            List<User> allUsers = _masterContext.Users.ToList();
            var users = _mapper.Map<List<UserRespondDTO>>(allUsers);

            return users;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO userLoginDTO)
        {
            List<UserRespondDTO> users = GetAllUsers();

            bool exist = users.Any(x => x.UserName == userLoginDTO.UserName);

            if (!exist)
            {
                throw new NotImplementedException();
            }
            List<User> allUsers = _masterContext.Users.ToList();
            User user = allUsers.FirstOrDefault(x => x.UserName == userLoginDTO.UserName);
            bool login = PasswordTools.VerifyPassword(userLoginDTO, user);
            if (!login)
            {
                return "Incorrect credentials";
            }

            string token = _tokenService.CreateToken(user);

            return Ok(token);
        }
    }
}