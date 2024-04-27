﻿namespace Talabat.Core.Entities.Identity
{
	public class Adress : BaseEntity
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}