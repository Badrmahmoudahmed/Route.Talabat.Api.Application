using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Infrastructure.Identity
{
	public static class ApplicationIdentitySeeding
	{
		public static async Task SeedUserAsync (UserManager<ApplicationUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var user = new ApplicationUser()
				{
					DisplayName = "Badr Eldin",
					Email = "badrmahmoud201312@gmail.com",
					UserName = "Badr",
					PhoneNumber = "01091008932"
				};

				await userManager.CreateAsync (user , "123bB@");
			}
		}
	}
}
