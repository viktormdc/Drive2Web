using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.FacebookDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Data.Mapping.FacebookDatas
{
    public class FacebookDataCountryMap : IEntityTypeConfiguration<FacebookDataCountry>
    {
        public void Configure(EntityTypeBuilder<FacebookDataCountry> builder)
        {
            builder.ToTable("FacebookDataCountry", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<FacebookData>(x => x.FacebookData)
                .WithMany(x => x.FacebookDataCountry).HasForeignKey(x => x.FacebookDataId).IsRequired();
        }
     
    }
}
