using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace NextowIdentity2020.Data
{
    public class NextowDbContext:IdentityDbContext
    {
        public NextowDbContext(DbContextOptions <NextowDbContext> options):base(options) { 
        
        }
    }
}
