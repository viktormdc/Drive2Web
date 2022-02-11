using Analytics.Data.Domain.Ads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.Ads
{
    public class AdMap : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> entity)
        {

            entity.ToTable("Ad", "als");
            entity.HasKey(x => x.Id);

        }
    }
}
