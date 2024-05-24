using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myapp.Interface;
using myapp.Models;

namespace myapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public string Login([FromBody] LoginRequest loginModel)
        {
            var result = _loginService.Login(loginModel);
            return result;
        }
    }
}
