using BancoDigital.Data;
using BancoDigital.DTOs;
using BancoDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDigital.Controllers;

[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _db;

    public ClientesController(AppDbContext db)
    {
        _db = db;
    }

    // POST /api/clientes/pf
    [HttpPost("pf")]
    public async Task<IActionResult> CadastrarPF([FromBody] CriarPessoaFisicaRequest req)
    {
        // Verifica se agência existe
        var agencia = await _db.Agencias.FindAsync(req.AgenciaId);
        if (agencia == null)
            return NotFound(new { mensagem = "Agência não encontrada." });

        // Verifica CPF duplicado
        var cpfExiste = await _db.PessoasFisicas.AnyAsync(pf => pf.CPF == req.CPF);
        if (cpfExiste)
            return BadRequest(new { mensagem = "CPF já cadastrado." });

        var pf = new PessoaFisica
        {
            Nome = req.Nome,
            Email = req.Email,
            Telefone = req.Telefone,
            CPF = req.CPF,
            DataNascimento = req.DataNascimento,
            AgenciaId = req.AgenciaId
        };

        _db.PessoasFisicas.Add(pf);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarCliente), new { id = pf.Id }, new ClienteResponse
        {
            Id = pf.Id,
            Nome = pf.Nome,
            Email = pf.Email,
            Tipo = "PF",
            AgenciaId = pf.AgenciaId,
            CPF = pf.CPF
        });
    }

    // POST /api/clientes/pj
    [HttpPost("pj")]
    public async Task<IActionResult> CadastrarPJ([FromBody] CriarPessoaJuridicaRequest req)
    {
        // Verifica se agência existe
        var agencia = await _db.Agencias.FindAsync(req.AgenciaId);
        if (agencia == null)
            return NotFound(new { mensagem = "Agência não encontrada." });

        // Verifica CNPJ duplicado
        var cnpjExiste = await _db.PessoasJuridicas.AnyAsync(pj => pj.CNPJ == req.CNPJ);
        if (cnpjExiste)
            return BadRequest(new { mensagem = "CNPJ já cadastrado." });

        var pj = new PessoaJuridica
        {
            Nome = req.Nome,
            Email = req.Email,
            Telefone = req.Telefone,
            CNPJ = req.CNPJ,
            RazaoSocial = req.RazaoSocial,
            AgenciaId = req.AgenciaId
        };

        _db.PessoasJuridicas.Add(pj);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(BuscarCliente), new { id = pj.Id }, new ClienteResponse
        {
            Id = pj.Id,
            Nome = pj.Nome,
            Email = pj.Email,
            Tipo = "PJ",
            AgenciaId = pj.AgenciaId,
            CNPJ = pj.CNPJ,
            RazaoSocial = pj.RazaoSocial
        });
    }

    // GET /api/clientes/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarCliente(int id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente == null)
            return NotFound(new { mensagem = "Cliente não encontrado." });

        var response = new ClienteResponse
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Email = cliente.Email,
            AgenciaId = cliente.AgenciaId
        };

        // Verifica qual tipo de cliente é
        if (cliente is PessoaFisica pf)
        {
            response.Tipo = "PF";
            response.CPF = pf.CPF;
        }
        else if (cliente is PessoaJuridica pj)
        {
            response.Tipo = "PJ";
            response.CNPJ = pj.CNPJ;
            response.RazaoSocial = pj.RazaoSocial;
        }

        return Ok(response);
    }
}
