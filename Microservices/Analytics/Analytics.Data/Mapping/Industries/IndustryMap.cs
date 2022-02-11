using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Industries;
using Analytics.Data.Domain.InstagramDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Data.Mapping.Industries
{
    public class IndustryMap : IEntityTypeConfiguration<Industry>
    {
        public void Configure(EntityTypeBuilder<Industry> builder)
        {
            builder.ToTable("Industry", "als");
            builder.HasKey(x => x.Id);

        }
    }
}
