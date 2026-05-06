using Microsoft.EntityFrameworkCore;
using MadFlix.Models;

namespace MadFlix.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Ator> Atores { get; set; }
    public DbSet<Diretor> Diretores { get; set; }
    public DbSet<FilmeAtor> FilmeAtores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Filme tiene un Diretor
        modelBuilder.Entity<Filme>()
            .HasOne(f => f.Diretor)
            .WithMany(d => d.Filmes)
            .HasForeignKey(f => f.DiretorId);

        // Filme <-> Ator (relación N:M)
        modelBuilder.Entity<FilmeAtor>()
            .HasKey(fa => new { fa.FilmeId, fa.AtorId });

        modelBuilder.Entity<FilmeAtor>()
            .HasOne(fa => fa.Filme)
            .WithMany(f => f.FilmeAtores)
            .HasForeignKey(fa => fa.FilmeId);

        modelBuilder.Entity<FilmeAtor>()
            .HasOne(fa => fa.Ator)
            .WithMany(a => a.FilmeAtores)
            .HasForeignKey(fa => fa.AtorId);
    }
}