using Analytics.Data.Domain.InstagramDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.InstagramDatas
{
	public class InstagramDataCountryMap : IEntityTypeConfiguration<InstagramDataCountry>
	{
		public void Configure(EntityTypeBuilder<InstagramDataCountry> builder)
		{
			builder.ToTable("InstagramDataCountry", "als");
			builder.HasKey(x => x.Id);
			builder.HasOne<InstagramData>(x => x.InstagramData)
				.WithMany(x => x.InstagramDataCountry).HasForeignKey(x => x.InstagramDataId).IsRequired();
		}
	}
}
