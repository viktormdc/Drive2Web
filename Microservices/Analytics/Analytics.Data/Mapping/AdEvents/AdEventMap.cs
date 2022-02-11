using Analytics.Data.Domain.AdEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Ads;

namespace Analytics.Data.Mapping.AdEvents
{
    public class AdEventMap : IEntityTypeConfiguration<AdEvent>
    {
        public void Configure(EntityTypeBuilder<AdEvent> builder)
        {

            builder.ToTable("AdEvent", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<Ad>(x => x.Ad)
                .WithMany(x => x.AdEvents).HasForeignKey(x => x.AdId).IsRequired();
        }
    }
}
