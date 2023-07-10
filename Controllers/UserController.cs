using API_REST.DTOs.Request;
using API_REST.DTOs.Respond;
using API_REST.Exceptions;
using API_REST.Infrastructure.Data;
using API_REST.Infrastructure.Models;
using API_REST.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {   
        private readonly IRegisterInterface _register;
        private readonly MasterContext _masterContext;
        private readonly IMapper _mapper;
        public UserController(IRegisterInterface register, MasterContext masterContext, IMapper mapper) 
        { 
            _register = register;
            _masterContext = masterContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserRespondDTO>> PostNewUser(UserRequestDTO userRequestDTO)
        {
            if (!ModelState.IsValid) 
            { 
                throw new MissedDataException("Some neccesary data for registration is missing");
            }

            var user = _mapper.Map<User>(userRequestDTO);
   
            _masterContext.Add(user);

            await _masterContext.SaveChangesAsync();
            UserRespondDTO userRespondDTO = new UserRespondDTO();   
            userRespondDTO = _mapper.Map<UserRespondDTO>(user);

            return userRespondDTO;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            User user = new User();

            user = await _masterContext.Users.FirstOrDefaultAsync(x => x.Id == userId).ConfigureAwait(true);

            return user;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserRespondDTO>>> GetUsers()
        {
            List<User> allUsers = _masterContext.Users.ToList();
            var users = _mapper.Map<List<UserRespondDTO>>(allUsers);

            return users;
        }
    }
}