using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VsExample.Domain.Entities;

namespace VsExample.Infrastructure.Persistence.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        
        builder.HasKey(p => p.Id); 
        
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd(); 
        
        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100); 
        
        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("varchar(500)")
            .HasMaxLength(500);

        builder.Property(p => p.Price)
            .HasColumnName("price");
        
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at");
        
        builder.HasIndex(p => p.Name)
            .IsUnique();
    }
}
