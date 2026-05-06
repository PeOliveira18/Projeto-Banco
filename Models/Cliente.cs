namespace BancoDigital.Models;

// =============================================
// CLIENTE - Classe base (abstrata)
// Usamos herança com Discriminator do EF Core
// =============================================
public abstract class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    // Chave estrangeira para Agência
    public int AgenciaId { get; set; }
    public Agencia? Agencia { get; set; }

    // Um cliente pode ter várias contratações
    public List<Contratacao> Contratacoes { get; set; } = new();
}

// =============================================
// PESSOA FISICA - herda de Cliente
// =============================================
public class PessoaFisica : Cliente
{
    public string CPF { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
}

// =============================================
// PESSOA JURIDICA - herda de Cliente
// =============================================
public class PessoaJuridica : Cliente
{
    public string CNPJ { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
}
