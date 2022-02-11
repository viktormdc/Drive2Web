using Analytics.Data.Domain.GoogleDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.GoogleDatas
{
   public  class GoogleDataAgeMap : IEntityTypeConfiguration<GoogleDataAge>
    {
        public void Configure(EntityTypeBuilder<GoogleDataAge> builder)
        {
            builder.ToTable("GoogleDataAge", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<GoogleData>(x => x.GoogleData)
                .WithMany(x => x.GoogleDataAge).HasForeignKey(x => x.GoogleDataId).IsRequired();
        }
    }
}

