using Analytics.Data.Domain.SearchFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Data.Domain.Ads;

namespace Analytics.Data.Mapping.SearchFilters
{
    public class SearchFilterAdsMap : IEntityTypeConfiguration<SearchFilterAds>
    {
        public void Configure(EntityTypeBuilder<SearchFilterAds> builder)
        {

            builder.ToTable("SearchFilterAds", "als");
            builder.HasKey(x => x.Id);
            builder.HasOne<Ad>(x => x.Ad)
                .WithMany(x => x.SearchFilterAds).HasForeignKey(x => x.AdId).IsRequired();
            builder.HasOne<SearchFilter>(x => x.SearchFilter)
                .WithMany(x => x.SearchFilterAds).HasForeignKey(x => x.SearchFilterId).IsRequired();

        }
    }
}
