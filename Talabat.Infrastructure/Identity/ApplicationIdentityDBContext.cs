using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Infrastructure.Identity
{
	public class ApplicationIdentityDBContext : IdentityDbContext<ApplicationUser>
	{
        public ApplicationIdentityDBContext(DbContextOptions<ApplicationIdentityDBContext> options):base(options) 
        {
            
        }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Adress>().ToTable("Adresses");
		}
	}
}
