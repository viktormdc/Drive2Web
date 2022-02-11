using Analytics.Data.Domain.InstagramDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.InstagramDatas
{
    public class InstagramDataCityMap : IEntityTypeConfiguration<InstagramDataCity>
    {
        public void Configure(EntityTypeBuilder<InstagramDataCity> builder)
        {
            builder.ToTable("InstagramDataCity", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<InstagramData>(x => x.InstagramData)
                .WithMany(x => x.InstagramDataCity).HasForeignKey(x => x.InstagramDataId).IsRequired();
        }
    }
}
