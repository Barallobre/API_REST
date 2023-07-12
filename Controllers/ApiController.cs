using API_REST.DTOs.Request;
using API_REST.DTOs.Respond;
using API_REST.Exceptions;
using API_REST.Infrastructure.Data;
using API_REST.Infrastructure.Models;
using API_REST.Interfaces;
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
        public ApiController(IRegisterInterface register, MasterContext masterContext, IMapper mapper) 
        { 
            _register = register;
            _masterContext = masterContext;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRespondDTO>> RegisterNewUser(UserRequestDTO userRequestDTO)
        {
            //if (!ModelState.IsValid) 
            //{ 
            //    throw new MissedDataException("Some neccesary data for registration is missing");
            //}
            try
            {
                
                var userRespondDTO = await _register.Registration(userRequestDTO).ConfigureAwait(true);

                return userRespondDTO;
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
        public async Task<ActionResult<string>> Login(string userName, string password)
        {
            List<UserRespondDTO> users = GetAllUsers();

            bool exist = users.Any(x => x.UserName == userName);

            if (!exist)
            {
                throw new NotImplementedException();
            }
            List<User> allUsers = _masterContext.Users.ToList();
            bool login = PasswordTools.VerifyPassword(password, userName, allUsers);
            if (!login)
            {
                return "Incorrect credentials";
            }
            return "You are logged in";
        }
    }
}