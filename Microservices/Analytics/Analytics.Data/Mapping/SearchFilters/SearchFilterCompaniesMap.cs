using Analytics.Data.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.SearchFilters
{
    public class SearchFilterCompaniesMap : IEntityTypeConfiguration<SearchFilterCompanies>
    {
        public void Configure(EntityTypeBuilder<SearchFilterCompanies> entity)
        {

            entity.ToTable("SearchFilterCompanies", "als");
            entity.HasKey(x => x.Id);

        }
    }
}
