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
    public DbSet<Utilizador> Utilizadores { get; set; }
    public DbSet<FilmeCompra> FilmeCompras { get; set; }
    public DbSet<FilmeVisualizacao> FilmeVisualizacoes { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
    public DbSet<ComentarioFilme> ComentariosFilmes { get; set; }

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

        modelBuilder.Entity<Utilizador>()
            .HasIndex(u => u.NomeUtilizadorNormalizado)
            .IsUnique();

        modelBuilder.Entity<Utilizador>()
            .HasIndex(u => u.EmailNormalizado)
            .IsUnique();

        modelBuilder.Entity<FilmeCompra>()
            .HasIndex(fc => new { fc.UtilizadorId, fc.FilmeId })
            .IsUnique();

        modelBuilder.Entity<FilmeCompra>()
            .HasOne(fc => fc.Utilizador)
            .WithMany(u => u.Compras)
            .HasForeignKey(fc => fc.UtilizadorId);

        modelBuilder.Entity<FilmeCompra>()
            .HasOne(fc => fc.Filme)
            .WithMany(f => f.Compras)
            .HasForeignKey(fc => fc.FilmeId);

        modelBuilder.Entity<FilmeVisualizacao>()
            .HasIndex(fv => new { fv.UtilizadorId, fv.FilmeId })
            .IsUnique();

        modelBuilder.Entity<FilmeVisualizacao>()
            .HasOne(fv => fv.Utilizador)
            .WithMany(u => u.Visualizacoes)
            .HasForeignKey(fv => fv.UtilizadorId);

        modelBuilder.Entity<FilmeVisualizacao>()
            .HasOne(fv => fv.Filme)
            .WithMany(f => f.Visualizacoes)
            .HasForeignKey(fv => fv.FilmeId);

        modelBuilder.Entity<Carrinho>()
            .HasIndex(c => c.UtilizadorId)
            .IsUnique();

        modelBuilder.Entity<Carrinho>()
            .HasOne(c => c.Utilizador)
            .WithOne(u => u.Carrinho)
            .HasForeignKey<Carrinho>(c => c.UtilizadorId);

        modelBuilder.Entity<CarrinhoItem>()
            .HasOne(ci => ci.Carrinho)
            .WithMany(c => c.Itens)
            .HasForeignKey(ci => ci.CarrinhoId);

        modelBuilder.Entity<CarrinhoItem>()
            .HasOne(ci => ci.Festival)
            .WithMany(f => f.CarrinhoItens)
            .HasForeignKey(ci => ci.FestivalId);

        modelBuilder.Entity<CarrinhoItem>()
            .HasOne(ci => ci.Filme)
            .WithMany(f => f.CarrinhoItens)
            .HasForeignKey(ci => ci.FilmeId);

        modelBuilder.Entity<ComentarioFilme>()
            .HasOne(comentario => comentario.Filme)
            .WithMany(filme => filme.Comentarios)
            .HasForeignKey(comentario => comentario.FilmeId);

        modelBuilder.Entity<ComentarioFilme>()
            .HasOne(comentario => comentario.Utilizador)
            .WithMany(utilizador => utilizador.ComentariosFilmes)
            .HasForeignKey(comentario => comentario.UtilizadorId);
    }
}
