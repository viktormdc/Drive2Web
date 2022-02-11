using Analytics.Data.Domain.Companies;
using Analytics.Data.Domain.Industries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.Companies
{
    public class CompanyMap: IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company", "als");
            builder.HasKey(x => x.Id);
            //builder.HasOne<Industry>(x => x.Industry)
            //    .WithMany(x => x.Company).HasForeignKey(x => x.IndustryId).IsRequired();

        }
    }
}
