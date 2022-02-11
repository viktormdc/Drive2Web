using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.FacebookDatas;
using Analytics.Data.Domain.SocialNetworks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Data.Mapping.FacebookDatas
{
    public class FacebookDataMap : IEntityTypeConfiguration<FacebookData>
    {

        public void Configure(EntityTypeBuilder<FacebookData> builder)
        {
            builder.ToTable("FacebookData", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<SocialNetwork>(x => x.SocialNetwork)
                .WithMany(x => x.FacebookData).HasForeignKey(x => x.SocialNetworkId).IsRequired();
        }
    }
}
