using Analytics.Data.Domain.InstagramDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.InstagramDatas
{
    public class InstagramDataGenderAndAgeMap : IEntityTypeConfiguration<InstagramDataGenderAndAge>
	{
		public void Configure(EntityTypeBuilder<InstagramDataGenderAndAge> builder)
		{
			builder.ToTable("InstagramDataGenderAndAge", "als");
			builder.HasKey(x => x.Id);
			builder.HasOne<InstagramData>(x => x.InstagramData)
				.WithMany(x => x.InstagramDataGenderAndAge).HasForeignKey(x => x.InstagramDataId).IsRequired();
		}
	}
}
