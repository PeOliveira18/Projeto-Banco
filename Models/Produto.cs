namespace BancoDigital.Models;

// =============================================
// PRODUTO - Classe base (abstrata)
// Produto escolhido: EMPRESTIMO
// =============================================
public abstract class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

// Produto: Empréstimo
public class Emprestimo : Produto
{
    public decimal ValorSolicitado { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal TaxaJurosMensal { get; set; } // Ex: 0.02 = 2%

    // Calcula o valor da parcela (regra de negócio)
    public decimal CalcularParcela()
    {
        // Fórmula de juros compostos: PMT = PV * i / (1 - (1+i)^-n)
        if (TaxaJurosMensal == 0)
            return ValorSolicitado / NumeroParcelas;

        double i = (double)TaxaJurosMensal;
        double n = NumeroParcelas;
        double pv = (double)ValorSolicitado;
        double parcela = pv * i / (1 - Math.Pow(1 + i, -n));
        return Math.Round((decimal)parcela, 2);
    }
}

// =============================================
// CONTRATACAO - Liga cliente a um produto
// =============================================
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

    // Relacionamentos
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    // Dados do empréstimo solicitado (salvos na contratação)
    public decimal ValorSolicitado { get; set; }
    public int NumeroParcelas { get; set; }
    public decimal TaxaJuros { get; set; }
    public decimal? ValorParcela { get; set; } // calculado pelo consumer
}
