using minha_api.src.Enums;
namespace minha_api.src.Dto;


public class AdministradorDTO
{
    public string Email { get;set; } = default!;
    public string Senha { get;set; } = default!;
    public Perfil? Perfil { get;set; } = default!;  
}
