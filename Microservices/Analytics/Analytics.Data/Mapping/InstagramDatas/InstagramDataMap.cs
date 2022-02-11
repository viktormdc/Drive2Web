using Analytics.Data.Domain.InstagramDatas;
using Analytics.Data.Domain.SocialNetworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.InstagramDatas
{

    public class InstagramDataMap : IEntityTypeConfiguration<InstagramData>
    {

        public void Configure(EntityTypeBuilder<InstagramData> builder)
        {
            builder.ToTable("InstagramData", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<SocialNetwork>(x => x.SocialNetwork)
                .WithMany(x => x.InstagramData).HasForeignKey(x => x.SocialNetworkId).IsRequired();
        }
    }

}
