using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Book2App.Models;

namespace Book2App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book2App.Models.Book> Book { get; set; }
        public DbSet<Book2App.Models.Order> Orders { get; set; }
        public DbSet<Book2App.Models.Customer> Customers { get; set; }
    }
}
