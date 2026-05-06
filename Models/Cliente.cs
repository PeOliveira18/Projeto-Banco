namespace BancoDigital.Models;

public abstract class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    public int AgenciaId { get; set; }
    public Agencia? Agencia { get; set; }

    public List<Contratacao> Contratacoes { get; set; } = new();
}

public class PessoaFisica : Cliente
{
    public string CPF { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
}

public class PessoaJuridica : Cliente
{
    public string CNPJ { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
}
