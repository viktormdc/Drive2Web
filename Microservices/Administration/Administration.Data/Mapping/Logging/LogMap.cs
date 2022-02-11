using System;
using System.Collections.Generic;
using System.Text;
using Administration.Data.Domain.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Data.Mapping.Logging
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Map(EntityTypeBuilder<Log> builder)
        {
           
        }

        public void Configure(EntityTypeBuilder<Log> entity)
        {
            entity.ToTable("Log");
            entity.HasKey(x => x.Id);
          

        }
    }
}