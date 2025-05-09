using Microsoft.EntityFrameworkCore;
using projet.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace projet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles")); // Optional: specify join table name
  
  
    modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    // Validation des propriétés de l'entité User
    modelBuilder.Entity<User>()
        .Property(u => u.Username)
        .IsRequired() // La propriété Username est requise
        .HasMaxLength(150); // Limite la longueur à 150 caractères (si nécessaire)

    modelBuilder.Entity<User>()
        .Property(u => u.Email)
        .IsRequired() // La propriété Email est requise
        .HasMaxLength(255) 
        // Limite la longueur de l'email à 255 caractères
        .HasAnnotation("Email", "true"); // Optionnel : assurez-vous que le champ email est un email valide
       

    modelBuilder.Entity<User>()
        .Property(u => u.Password)
        .IsRequired(); // Le mot de passe est requis

    // Validation des propriétés de l'entité Post
    modelBuilder.Entity<Post>()
        .Property(p => p.Content)
        .IsRequired() // Le contenu est requis
        .HasMaxLength(1000); // Limite le contenu à 1000 caractères, à ajuster selon vos besoins

    // Validation des propriétés de l'entité Role
    modelBuilder.Entity<Role>()
        .Property(r => r.Name)
        .IsRequired() // Le nom est requis
       
        .HasMaxLength(100); // Limite le nom à 100 caractères
    }
    }


}
