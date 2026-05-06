using BancoDigital.Data;
using BancoDigital.DTOs;
using BancoDigital.Models;
using Microsoft.AspNetCore.Mvc;

namespace BancoDigital.Controllers;

[ApiController]
[Route("api/agencias")]
public class AgenciasController : ControllerBase
{
    private readonly AppDbContext _db;

    public AgenciasController(AppDbContext db)
    {
        _db = db;
    }

    // POST /api/agencias
    [HttpPost]
    public async Task<IActionResult> CadastrarAgencia([FromBody] CriarAgenciaRequest req)
    {
        var agencia = new Agencia
        {
            Nome = req.Nome,
            Cidade = req.Cidade,
            Numero = req.Numero
        };

        _db.Agencias.Add(agencia);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarAgencia), new { id = agencia.Id }, new AgenciaResponse
        {
            Id = agencia.Id,
            Nome = agencia.Nome,
            Cidade = agencia.Cidade,
            Numero = agencia.Numero
        });
    }

    // GET /api/agencias/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarAgencia(int id)
    {
        var agencia = await _db.Agencias.FindAsync(id);
        if (agencia == null)
            return NotFound(new { mensagem = "Agência não encontrada." });

        return Ok(new AgenciaResponse
        {
            Id = agencia.Id,
            Nome = agencia.Nome,
            Cidade = agencia.Cidade,
            Numero = agencia.Numero
        });
    }
}
