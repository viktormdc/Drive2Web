using Microsoft.EntityFrameworkCore;
using Analytics.Data.Domain.TwitterDatas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.SocialNetworks;

namespace Analytics.Data.Mapping.TwitterDatas
{
    public class TwitterDataMap : IEntityTypeConfiguration<TwitterData>
    {       
        public void Configure(EntityTypeBuilder<TwitterData> builder)
        {  
            builder.ToTable("TwitterData", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<SocialNetwork>(x => x.SocialNetwork)
                .WithMany(x => x.TwitterData).HasForeignKey(x => x.SocialNetworkId).IsRequired();
        }
    }
}
