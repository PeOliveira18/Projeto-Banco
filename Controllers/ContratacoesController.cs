using BancoDigital.Data;
using BancoDigital.DTOs;
using BancoDigital.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoDigital.Controllers;

[ApiController]
[Route("api/contratacoes")]
public class ContratacoesController : ControllerBase
{
    private readonly AppDbContext _db;

    public ContratacoesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> SolicitarContratacao([FromBody] CriarContratacaoRequest req)
    {
        var cliente = await _db.Clientes.FindAsync(req.ClienteId);
        if (cliente == null)
            return NotFound(new { mensagem = "Cliente não encontrado." });

        decimal valorParcela = CalcularParcela(req.ValorSolicitado, req.NumeroParcelas, req.TaxaJuros);

        var contratacao = new Contratacao
        {
            ClienteId = req.ClienteId,
            ValorSolicitado = req.ValorSolicitado,
            NumeroParcelas = req.NumeroParcelas,
            TaxaJuros = req.TaxaJuros,
            ValorParcela = valorParcela,
            Status = StatusContratacao.Aprovado,
            DataSolicitacao = DateTime.Now
        };

        _db.Contratacoes.Add(contratacao);
        await _db.SaveChangesAsync();

        return StatusCode(201, new ContratacaoResponse
        {
            Id = contratacao.Id,
            ClienteId = contratacao.ClienteId,
            Status = contratacao.Status.ToString(),
            ValorSolicitado = contratacao.ValorSolicitado,
            NumeroParcelas = contratacao.NumeroParcelas,
            ValorParcela = contratacao.ValorParcela,
            DataSolicitacao = contratacao.DataSolicitacao
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ConsultarContratacao(int id)
    {
        var contratacao = await _db.Contratacoes.FindAsync(id);
        if (contratacao == null)
            return NotFound(new { mensagem = "Contratação não encontrada." });

        return Ok(new ContratacaoResponse
        {
            Id = contratacao.Id,
            ClienteId = contratacao.ClienteId,
            Status = contratacao.Status.ToString(),
            ValorSolicitado = contratacao.ValorSolicitado,
            NumeroParcelas = contratacao.NumeroParcelas,
            ValorParcela = contratacao.ValorParcela,
            DataSolicitacao = contratacao.DataSolicitacao
        });
    }

    private decimal CalcularParcela(decimal valor, int parcelas, decimal taxa)
    {
        if (taxa == 0)
            return Math.Round(valor / parcelas, 2);

        double i = (double)taxa;
        double n = parcelas;
        double pv = (double)valor;
        double pmt = pv * i / (1 - Math.Pow(1 + i, -n));
        return Math.Round((decimal)pmt, 2);
    }
}
