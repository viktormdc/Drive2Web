using Analytics.Data.Domain.GoogleDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.GoogleDatas
{
    public class GoogleDataCountryMap : IEntityTypeConfiguration<GoogleDataCountry>
    {
        public void Configure(EntityTypeBuilder<GoogleDataCountry> builder)
        {
            builder.ToTable("GoogleDataCountry", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<GoogleData>(x => x.GoogleData)
                .WithMany(x => x.GoogleDataCountry).HasForeignKey(x => x.GoogleDataId).IsRequired();
        }
    }
}
