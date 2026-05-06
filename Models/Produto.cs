namespace BancoDigital.Models;

public abstract class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

public class Emprestimo : Produto
{
    public decimal ValorSolicitado { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal TaxaJurosMensal { get; set; }

    public decimal CalcularParcela()
    {
        if (TaxaJurosMensal == 0)
            return ValorSolicitado / NumeroParcelas;

        double i = (double)TaxaJurosMensal;
        double n = NumeroParcelas;
        double pv = (double)ValorSolicitado;
        double parcela = pv * i / (1 - Math.Pow(1 + i, -n));
        return Math.Round((decimal)parcela, 2);
    }
}

public enum StatusContratacao
{
    Pendente,
    Aprovado,
    Reprovado
}

public class Contratacao
{
    public int Id { get; set; }
    public DateTime DataSolicitacao { get; set; } = DateTime.Now;
    public StatusContratacao Status { get; set; } = StatusContratacao.Pendente;

    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public decimal ValorSolicitado { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal TaxaJuros { get; set; }
    public decimal? ValorParcela { get; set; }
}
