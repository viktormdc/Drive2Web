using Analytics.Data.Domain.LinkedInDatas;
using Analytics.Data.Domain.SocialNetworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.LinkedinDatas
{
    public class LinkedinDataMap : IEntityTypeConfiguration<LinkedInData>
    {
        public void Configure(EntityTypeBuilder<LinkedInData> builder)
        {
            builder.ToTable("LinkedinData", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<SocialNetwork>(x => x.SocialNetwork)
                .WithMany(x => x.LinkedInData).HasForeignKey(x => x.SocialNetworkId).IsRequired();
        }

    }
}
