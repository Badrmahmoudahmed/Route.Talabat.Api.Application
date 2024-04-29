﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Api.Dtos;
using Route.Talabat.Api.ErrorsHandler;
using Route.Talabat.Api.Extenstion;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IAuthService authService, IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
			_mapper = mapper;
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
				Token = await _authService.CreateTokenAsync(user, _userManager)
			}) ;

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
				Token = await _authService.CreateTokenAsync(user, _userManager)
			});
		}
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = await _userManager.FindByIdAsync(email);

			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManager),
			});
		}

		[HttpGet("adress")]
		public async Task<ActionResult<AdressDto>> GetUserAdress()
		{
			var user = await _userManager.FindUserWithAdressByEmailAsync(User);
			return Ok(_mapper.Map<Adress,AdressDto>(user.Adress));
		}

		[HttpPut("adress")]
		public async Task<ActionResult<Adress>> UpdateUserAdress(AdressDto adress)
		{
			var updatedadress =  _mapper.Map<AdressDto, Adress>(adress);
			var user = await _userManager.FindUserWithAdressByEmailAsync(User);

			updatedadress.Id = user.Adress.Id;
			user.Adress = updatedadress;

			var result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded) return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(e => e.Description)});
			return Ok(updatedadress);
		}
	}
}
