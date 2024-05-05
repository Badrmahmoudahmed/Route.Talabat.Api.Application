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
	internal class DeliveryMethodCofiguration : IEntityTypeConfiguration<DeliveryMethod>
	{
		public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
		{
			builder.Property(D => D.Cost).HasColumnType("decimal(12,2)");
		}
	}
}
