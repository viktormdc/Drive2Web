using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.FacebookDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Data.Mapping.FacebookDatas
{
    public class FacebookDataGenderAndAgeMap : IEntityTypeConfiguration<FacebookDataGenderAndAge>
    {
        public void Configure(EntityTypeBuilder<FacebookDataGenderAndAge> builder)
        {
            builder.ToTable("FacebookDataGenderAndAge", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<FacebookData>(x => x.FacebookData)
                .WithMany(x => x.FacebookDataGenderAndAge).HasForeignKey(x => x.FacebookDataId).IsRequired();
        }
       
    }
}

