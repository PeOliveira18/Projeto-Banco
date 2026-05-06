using BancoDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoDigital.Data;

// =============================================
// APP DB CONTEXT
// Configura todas as entidades do banco
// =============================================
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Tabelas
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<PessoaFisica> PessoasFisicas { get; set; }
    public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
    public DbSet<Agencia> Agencias { get; set; }
    public DbSet<Contratacao> Contratacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =============================================
        // HERANÇA: TPH (Table Per Hierarchy)
        // Uma só tabela CLIENTES com coluna discriminadora
        // =============================================
        modelBuilder.Entity<Cliente>()
            .HasDiscriminator<string>("TipoCliente")
            .HasValue<PessoaFisica>("PF")
            .HasValue<PessoaJuridica>("PJ");

        // CPF e CNPJ únicos (não pode duplicar)
        modelBuilder.Entity<PessoaFisica>()
            .HasIndex(pf => pf.CPF)
            .IsUnique();

        modelBuilder.Entity<PessoaJuridica>()
            .HasIndex(pj => pj.CNPJ)
            .IsUnique();

        // Cliente pertence a uma Agência
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Agencia)
            .WithMany(a => a.Clientes)
            .HasForeignKey(c => c.AgenciaId);

        // Contratação pertence a um Cliente
        modelBuilder.Entity<Contratacao>()
            .HasOne(ct => ct.Cliente)
            .WithMany(c => c.Contratacoes)
            .HasForeignKey(ct => ct.ClienteId);

        // Nomes das tabelas no Oracle (MAIÚSCULO por padrão)
        modelBuilder.Entity<Cliente>().ToTable("TB_CLIENTES");
        modelBuilder.Entity<Agencia>().ToTable("TB_AGENCIAS");
        modelBuilder.Entity<Contratacao>().ToTable("TB_CONTRATACOES");
    }
}
