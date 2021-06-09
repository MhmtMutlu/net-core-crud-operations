using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // DbContext is a class which is coming with EntityFramework and it allows us to do crud operations easily
    public class UserContext : DbContext
    {
        // Used Npgssql and EntityFrameworkCore to create connection string for databes
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host = localhost; Database = crudoperations; Username = postgres; Password = 1234");
        }
            // Matching User class and Users data table
            public DbSet<User> Users { get; set; }
    }
}
