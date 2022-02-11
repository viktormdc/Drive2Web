using Analytics.Data.Domain.Clinets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Analytics.Data.Mapping.Clients
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.ToTable("Client", "als");
            builder.HasKey(x => x.Id);
       
        }
    }
}
