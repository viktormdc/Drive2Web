using Analytics.Data.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.SearchFilters
{
    public class SearchFilterBrandsMap : IEntityTypeConfiguration<SearchFilterBrands>
    {
        public void Configure(EntityTypeBuilder<SearchFilterBrands> entity)
        {

            entity.ToTable("SearchFilterBrands", "als");
            entity.HasKey(x => x.Id);

        }
    }
}
