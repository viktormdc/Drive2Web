using Analytics.Data.Domain.GoogleDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.GoogleDatas
{
    public class GoogleDataCityMap : IEntityTypeConfiguration<GoogleDataCity>
    {
        public void Configure(EntityTypeBuilder<GoogleDataCity> builder)
        {
            builder.ToTable("GoogleDataCity", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<GoogleData>(x => x.GoogleData)
                .WithMany(x => x.GoogleDataCity).HasForeignKey(x => x.GoogleDataId).IsRequired();
        }
    }
}
