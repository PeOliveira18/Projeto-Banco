namespace BancoDigital.DTOs;

public class CriarPessoaFisicaRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public int AgenciaId { get; set; }
}

public class CriarPessoaJuridicaRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    public int AgenciaId { get; set; }
}

public class CriarAgenciaRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
}

public class CriarContratacaoRequest
{
    public int ClienteId { get; set; }
    public decimal ValorSolicitado { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal TaxaJuros { get; set; }
}

public class ClienteResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int AgenciaId { get; set; }
    public string? CPF { get; set; }
    public string? CNPJ { get; set; }
    public string? RazaoSocial { get; set; }
}

public class AgenciaResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
}

public class ContratacaoResponse
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal ValorSolicitado { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal? ValorParcela { get; set; }
    public DateTime DataSolicitacao { get; set; }
}
