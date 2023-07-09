using API_REST.Enums;
using API_REST.Exceptions;
using API_REST.Infrastructure.Data;
using API_REST.Infrastructure.Models;
using API_REST.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {   
        private readonly IRegisterInterface _register;
        private readonly MasterContext _masterContext;
        public UserController(IRegisterInterface register, MasterContext masterContext) 
        { 
            _register = register;
            _masterContext = masterContext;
        }

        [HttpPost]
        public User Post(string password, string firstName, string lastName, string email, string userName)
        {
            if (password == null || firstName == null || lastName == null || email == null) 
            { 
                throw new MissedDataException("Some neccesary data for registration is missing");
            }
            User user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = userName,
                Password = password,
                Active = true,
                EntryDate = DateTime.UtcNow,
                UserTypeId = (int)UserTypeEnum.Ordinary
            };

            _masterContext.Add(user);

            return user;
        }

        [HttpGet]
        public User Get(int id)
        {
            User user = new User();

            user = _masterContext.Users.FirstOrDefault(x => x.Id == id);

            return user;
        }
    }
}