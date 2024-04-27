﻿using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.Api.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
        public string Phone { get; set; }
    }
}