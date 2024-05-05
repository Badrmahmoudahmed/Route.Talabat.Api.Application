using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OredrAggregate;

namespace Talabat.Infrastructure.Data.Config.Order_Config
{
	internal class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.OwnsOne(o => o.ShippingAdress, o => o.WithOwner());
			builder.Property(o => o.Statues).HasConversion(
					(o) => o.ToString(),
					(o) => (OrderStatues)Enum.Parse(typeof(OrderStatues), o)
				);

			builder.Property(o => o.SubTotal).HasColumnType("decimal(12,2)");
		}
	}
}
