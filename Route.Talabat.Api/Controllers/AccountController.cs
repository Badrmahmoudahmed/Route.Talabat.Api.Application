using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Api.Dtos;
using Route.Talabat.Api.ErrorsHandler;
using Talabat.Core.Entities.Identity;

namespace Route.Talabat.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user is null) return Unauthorized(new ApiResponse(401 , "Invaild Login"));
			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password , false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invaild Login"));
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = "This Will Be Token"
			});

		}

		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			var user = new ApplicationUser()
			{
				UserName = registerDto.Email.Split("@")[0],
				Email = registerDto.Email,
				PhoneNumber = registerDto.Phone,
				DisplayName = registerDto.DisplayName
			};
			var result = await _userManager.CreateAsync(user);
			if (!result.Succeeded) return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(e => e.Description) });
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = "This Will Be Token"
			});
		}
    }
}
