﻿using Microsoft.EntityFrameworkCore;

namespace myapp.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) :base(options) { }
        
        public DbSet<User> Users { get; set; }
    }
}
