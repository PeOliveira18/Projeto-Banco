namespace BancoDigital.Models;

public class Agencia
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;

    public List<Cliente> Clientes { get; set; } = new();
}
