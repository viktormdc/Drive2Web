using Analytics.Data.Domain.GoogleDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.GoogleDatas
{
    public class GoogleDataGenderMap : IEntityTypeConfiguration<GoogleDataGender>
    {
        public void Configure(EntityTypeBuilder<GoogleDataGender> builder)
        {
            builder.ToTable("GoogleDataGender", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<GoogleData>(x => x.GoogleData)
                .WithMany(x => x.GoogleDataGender).HasForeignKey(x => x.GoogleDataId).IsRequired();
        }
    }
}

