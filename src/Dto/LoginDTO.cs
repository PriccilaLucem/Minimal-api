namespace minha_api.src.Dto;

public class LoginDTO
{
    public string Email { get;set; } = default!;
    public string Senha { get;set; } = default!;

    public LoginDTO(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}