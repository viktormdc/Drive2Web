using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.FacebookDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Data.Mapping.FacebookDatas
{
    public class FacebookDataCityMap : IEntityTypeConfiguration<FacebookDataCity>
    {
        public void Configure(EntityTypeBuilder<FacebookDataCity> builder)
        {
            builder.ToTable("FacebookDataCity", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<FacebookData>(x => x.FacebookData)
                .WithMany(x => x.FacebookDataCity).HasForeignKey(x => x.FacebookDataId).IsRequired();
        }
   
    }
}
