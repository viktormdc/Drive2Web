using Analytics.Data.Domain.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.Channels
{
    public class ChannelMap : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {

            builder.ToTable("Channel", "als");
            builder.HasKey(x => x.Id);

        }
    }
}
