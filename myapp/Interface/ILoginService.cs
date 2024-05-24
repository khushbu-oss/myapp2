using myapp.Models;

namespace myapp.Interface
{
    public interface ILoginService
    {

        string Login(LoginRequest loginRequest);
    }
}
