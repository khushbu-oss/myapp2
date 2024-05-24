using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myapp.Interface;
using myapp.Models;

namespace myapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _regService;


        public RegisterController(IRegisterService registerService)
        {
            _regService = registerService;
        }

        [HttpPost]
        public User AddUser([FromBody] User value)
        {
            var user = _regService.AddUser(value);
            return user;
        }

        
    }
}
