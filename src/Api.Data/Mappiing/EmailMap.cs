using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappiing
{
    public class EmailMap : IEntityTypeConfiguration<EmailEntity>
    {
        public void Configure(EntityTypeBuilder<EmailEntity> builder)
        {
            builder.ToTable("Emails");
            builder.HasNoKey();
        }
    }
}