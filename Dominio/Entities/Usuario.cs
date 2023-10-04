namespace Dominio.Entities;
public class Usuario : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Correo { get; set; }
    public int IdPersonafk { get; set; }
    public Persona Persona { get; set; }

}
