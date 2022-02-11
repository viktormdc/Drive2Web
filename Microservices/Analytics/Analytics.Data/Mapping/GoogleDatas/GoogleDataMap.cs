using Analytics.Data.Domain.GoogleDatas;
using Analytics.Data.Domain.SocialNetworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.GoogleDatas
{
    public class GoogleDataMap : IEntityTypeConfiguration<GoogleData>
    {

        public void Configure(EntityTypeBuilder<GoogleData> builder)
        {
            builder.ToTable("GoogleData", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<SocialNetwork>(x => x.SocialNetwork)
                .WithMany(x => x.GoogleData).HasForeignKey(x => x.SocialNetworkId).IsRequired();
        }
    }
}
