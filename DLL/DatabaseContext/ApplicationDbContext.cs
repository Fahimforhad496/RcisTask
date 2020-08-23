using System;
using System.Collections.Generic;
using System.Text;
using DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DLL.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
