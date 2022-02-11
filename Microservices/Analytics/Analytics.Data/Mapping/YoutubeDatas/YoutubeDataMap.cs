using Analytics.Data.Domain.SocialNetworks;
using Analytics.Data.Domain.YoutubeDatas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.YoutubeDatas
{
    public class YoutubeDataMap : IEntityTypeConfiguration<YoutubeData>
    {

        public void Configure(EntityTypeBuilder<YoutubeData> builder)
        {
            builder.ToTable("YoutubeData", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<SocialNetwork>(x => x.SocialNetwork)
                .WithMany(x => x.YoutubeData).HasForeignKey(x => x.SocialNetworkId).IsRequired();
        }
    }
}
