using Analytics.Data.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.SearchFilters
{
   public class SearchFilterIndustrysMap : IEntityTypeConfiguration<SearchFilterIndustrys>
    {
        public void Configure(EntityTypeBuilder<SearchFilterIndustrys> entity)
        {

            entity.ToTable("SearchFilterIndustrys", "als");
            entity.HasKey(x => x.Id);

        }
    }
}
