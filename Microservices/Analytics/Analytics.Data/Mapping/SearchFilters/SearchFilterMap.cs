using Analytics.Data.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Clinets;

namespace Analytics.Data.Mapping.SearchFilters
{
   public class SearchFilterMap : IEntityTypeConfiguration<SearchFilter>
    {
        public void Configure(EntityTypeBuilder<SearchFilter> builder)
        {

            builder.ToTable("SearchFilter", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<Client>(x => x.Client)
                .WithMany(x => x.SearchFilter).HasForeignKey(x => x.UserId).IsRequired();
        }
    }
}
