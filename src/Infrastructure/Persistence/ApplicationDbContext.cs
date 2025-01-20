using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VsExample.Domain.Entities;
using VsExample.Domain.Entities.Questionaire;

namespace VsExample.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Questionnaire> Questionnaires { get; set; }
    public DbSet<QuestionGroup> QuestionGroups { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<AnswerBase> Answers { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Product>().ToTable("products");
        //
        // modelBuilder.Entity<Product>()
        //     .HasKey(p => p.Id); 
        //
        // modelBuilder.Entity<Product>()
        //     .Property(p => p.Id)
        //     .HasColumnName("id")
        //     .ValueGeneratedOnAdd(); 
        //
        // modelBuilder.Entity<Product>()
        //     .Property(p => p.Name)
        //     .HasColumnName("name")
        //     .HasColumnType("varchar(100)")
        //     .HasMaxLength(100); 
        //
        // modelBuilder.Entity<Product>()
        //     .Property(p => p.Description)
        //     .HasColumnName("description")
        //     .HasColumnType("varchar(500)")
        //     .HasMaxLength(500);
        //
        // modelBuilder.Entity<Product>()
        //     .Property(p => p.Price)
        //     .HasColumnName("price");
        //
        // modelBuilder.Entity<Product>()
        //     .Property(p => p.CreatedAt)
        //     .HasColumnName("created_at");
        //
        // modelBuilder.Entity<Product>()
        //     .HasIndex(p => p.Name)
        //     .IsUnique(); 
    }
}