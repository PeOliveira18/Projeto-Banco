using BancoDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDigital.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<PessoaFisica> PessoasFisicas { get; set; }
    public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
    public DbSet<Agencia> Agencias { get; set; }
    public DbSet<Contratacao> Contratacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasDiscriminator<string>("TipoCliente")
            .HasValue<PessoaFisica>("PF")
            .HasValue<PessoaJuridica>("PJ");

        modelBuilder.Entity<PessoaFisica>()
            .HasIndex(pf => pf.CPF)
            .IsUnique();

        modelBuilder.Entity<PessoaJuridica>()
            .HasIndex(pj => pj.CNPJ)
            .IsUnique();

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Agencia)
            .WithMany(a => a.Clientes)
            .HasForeignKey(c => c.AgenciaId);

        modelBuilder.Entity<Contratacao>()
            .HasOne(ct => ct.Cliente)
            .WithMany(c => c.Contratacoes)
            .HasForeignKey(ct => ct.ClienteId);

        modelBuilder.Entity<Cliente>().ToTable("TB_CLIENTES");
        modelBuilder.Entity<Agencia>().ToTable("TB_AGENCIAS");
        modelBuilder.Entity<Contratacao>().ToTable("TB_CONTRATACOES");
    }
}
