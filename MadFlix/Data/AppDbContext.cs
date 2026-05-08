using MadFlix.Models;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<Festival> Festivais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>()
            .HasOne(f => f.Diretor)
            .WithMany(d => d.Filmes)
            .HasForeignKey(f => f.DiretorId);

        modelBuilder.Entity<Filme>()
            .HasOne(f => f.Festival)
            .WithMany(festival => festival.Filmes)
            .HasForeignKey(f => f.FestivalId);

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
