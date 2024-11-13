using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace minha_api.src.Entities;
public class Administrador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private int Id { get; set; } = default!;

    [Required]
    [StringLength(255)]
    private string Email { get; set; } = default!;

    [Required]
    [StringLength(50)]
    private string Senha { get; set; } = default!;

    [Required]
    [StringLength(10)]
    private string Perfil { get; set; } = default!;

    public int GetId() => Id;
    public string GetEmail() => Email;
    public string GetPerfil() => Perfil;
    public string GetSenha() => Senha;
    public void SetEmail(string email) => Email = email;
    public void SetSenha(string senha) => Senha = senha;
    public void SetPerfil(string perfil) => Perfil = perfil;


    public Administrador(string email, string senha, string perfil)
    {
        Email = email;
        Senha = senha;
        Perfil = perfil;
    }
}
