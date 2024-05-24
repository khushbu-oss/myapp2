using Microsoft.EntityFrameworkCore;
using myapp.Interface;
using myapp.Models;

namespace myapp.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly UserDbContext _dbcontext;

        // Constructor to initialize the RegisterService with UserDbContext
        public RegisterService(UserDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        // Method to check if an email already exists in the database
        public bool EmailExists(string email)
        {
            // Check if any user in the database has the provided email
            return _dbcontext.Users.Any(u => u.Email == email);
        }

        // Method to add a new user to the database
        public User AddUser(User user)
        {
            // Check if the email already exists
            if (EmailExists(user.Email))
            {
                // If email exists, throw exception
                throw new Exception("Email already exists");
            }

            // Add the user to the database
            var addedUser = _dbcontext.Add(user);

            // Save changes to the database
            _dbcontext.SaveChanges();

            // Return the added user entity
            return addedUser.Entity;
        }
    }
}
