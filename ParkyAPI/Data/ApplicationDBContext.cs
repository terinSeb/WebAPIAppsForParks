using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Modals;
namespace ParkyAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> Options) : base(Options)
        {

        }
        public DbSet<NationalParks> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<Users> users { get; set; }        
    }
}
