using API_REST.Exceptions;
using API_REST.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class User : ControllerBase
    {   
        private readonly IRegisterInterface _register;
        public User(IRegisterInterface register) 
        { 
            _register = register;
        }

        [HttpPost]
        public JsonContent Post(string password, string name, string lastName, string email)
        {
            if (password == null || name == null || lastName == null || email == null) 
            { 
                throw new MissedDataException("Some neccesary data for registration is missing");
            }
           
            return null;
        }
    }
}