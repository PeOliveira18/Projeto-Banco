namespace BancoDigital.Models;

// =============================================
// AGENCIA
// =============================================
public class Agencia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;

    // Uma agência pode ter vários clientes
    public List<Cliente> Clientes { get; set; } = new();
}
