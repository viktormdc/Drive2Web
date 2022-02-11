using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Brands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Data.Mapping.Brands
{
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> entity)
        {

            entity.ToTable("Brand", "als");
            entity.HasKey(x => x.Id);

        }
    }
}
