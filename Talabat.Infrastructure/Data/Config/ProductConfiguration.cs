using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Infrastructure.Data.Config
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(P => P.BrandId);
			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
			builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
			builder.HasOne(p => p.ProductCategory).WithMany().HasForeignKey(P => P.CategoryId);
		}
	}
}
