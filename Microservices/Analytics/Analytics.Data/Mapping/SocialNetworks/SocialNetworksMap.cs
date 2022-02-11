using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Analytics.Data.Domain.SocialNetworks;

namespace Analytics.Data.Mapping.SocialNetworks
{
    public class SocialNetworksMap : IEntityTypeConfiguration<SocialNetwork>
    {

        public void Configure(EntityTypeBuilder<SocialNetwork> builder)
        {
            builder.ToTable("SocialNetwork", "als");
            builder.HasKey(x => x.Id);
            
        }


    }
}
