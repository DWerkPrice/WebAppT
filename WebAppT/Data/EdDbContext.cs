using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppT.Models;

namespace WebAppT.Models
{
    public class EdDbContext : DbContext
    {
        public EdDbContext (DbContextOptions<EdDbContext> options)
            : base(options)
        {
        }
        public DbSet<Major> Majors { get; set; }
        public DbSet<WebAppT.Models.Major> Major { get; set; }
        public DbSet<WebAppT.Models.Student> Student { get; set; }
    }
}
