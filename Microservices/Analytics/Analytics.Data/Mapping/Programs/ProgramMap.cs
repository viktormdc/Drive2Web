using Analytics.Data.Domain.Programs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Data.Mapping.Programs
{
    public class ProgramMap : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> entity)
        {

            entity.ToTable("Programs", "als");
            entity.HasKey(x => x.Id);

        }
    }
}
