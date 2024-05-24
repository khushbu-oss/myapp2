using Microsoft.IdentityModel.Tokens;
using myapp.Interface;
using myapp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace myapp.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserDbContext _dbcontext;
        private readonly IConfiguration _configuration;

        // Constructor to initialize the LoginService with IConfiguration and UserDbContext
        public LoginService(IConfiguration configuration, UserDbContext dbcontext)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        // Method to authenticate a user and generate a JWT token
        public string Login(LoginRequest loginRequest)
        {
            // Check if email and password are provided
            if (loginRequest.Email != null && loginRequest.Password != null)
            {
                // Find user in the database by email and password
                var user = _dbcontext.Users.SingleOrDefault(s => s.Email == loginRequest.Email && s.Password == loginRequest.Password);

                // If user is found
                if (user != null)
                {
                    // Create claims for the JWT token including user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.Name),
                        new Claim("Email", user.Email),
                    };

                    // Create key using secret key from configuration
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    // Create signing credentials using the key and algorithm
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Create JWT token with issuer, audience, claims, expiration, and signing credentials
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    // Write JWT token as a string
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    // If user is not found, throw exception
                    throw new Exception("User is not valid");
                }
            }
            else
            {
                // If email or password is missing, throw exception
                throw new Exception("Email/Password is not valid");
            }
        }
    }
}
