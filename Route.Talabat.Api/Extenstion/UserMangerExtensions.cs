using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Route.Talabat.Api.Extenstion
{
	public static class UserMangerExtensions
	{
		public static Task<ApplicationUser> FindUserWithAdressByEmailAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = userManager.Users.Include(U => U.Adress).FirstOrDefaultAsync(U => U.NormalizedEmail == email.ToUpper());

			return user;
		}
	}
}
