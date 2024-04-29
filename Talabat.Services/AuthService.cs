using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
	public class AuthService : IAuthService
	{
		

	
        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
		{
			var userclaim = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.DisplayName),
				new Claim(ClaimTypes.Email, user.Email),
			};

			var userroles = await userManager.GetRolesAsync(user);
			foreach (var role in userroles) 
			{
				userclaim.Add(new Claim(ClaimTypes.Role, role));
			}

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StrongAuthKey"));
			var token = new JwtSecurityToken(
					audience:"MySecurityApiUser",
					issuer: "https://localhost:7142/",
					expires: DateTime.Now.AddDays(30),
					claims: userclaim,
					signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
