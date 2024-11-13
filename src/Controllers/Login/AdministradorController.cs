// AdministradoresEndpoints.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using minha_api.src.Interfaces;
using minha_api.src.Entities;
using minha_api.src.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class AdministradoresEndpoints
{
    public static void MapAdministradorEndpoints(IEndpointRouteBuilder endpoints, string key)
    {
        endpoints.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
        {
            var adm = administradorServico.Login(loginDTO);
            if (adm != null)
            {
                string token = GerarTokenJwt(adm, key);
                return Results.Ok({Token = token });
            }
            return Results.Unauthorized();
        }).AllowAnonymous().WithTags("Administradores");

        endpoints.MapGet("/administradores", ([FromQuery] int? pagina, IAdministradorServico administradorServico) =>
        {
            var administradores = administradorServico.Todos(pagina).Select(adm => new AdministradorModelView { Id = adm.Id, Email = adm.Email, Perfil = adm.Perfil }).ToList();
            return Results.Ok(administradores);
        }).RequireAuthorization("Adm").WithTags("Administradores");

        endpoints.MapGet("/Administradores/{id}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
        {
            var administrador = administradorServico.BuscaPorId(id);
            if (administrador == null) return Results.NotFound();
            return Results.Ok(new AdministradorModelView(administrador.Id,administrador.Email,administrador.Perfil));
        }).RequireAuthorization("Adm").WithTags("Administradores");

        endpoints.MapPost("/administradores", ([FromBody] AdministradorDTO administradorDTO, IAdministradorServico administradorServico) =>
        {
            if (string.IsNullOrEmpty(administradorDTO.Email) || string.IsNullOrEmpty(administradorDTO.Senha) || administradorDTO.Perfil == null)
                return Results.BadRequest(new { Message = "Preencha todos os campos." });

            var administrador = new Administrador { Email = administradorDTO.Email, Senha = administradorDTO.Senha, Perfil = administradorDTO.Perfil.ToString() ?? "Editor" };
            administradorServico.Incluir(administrador);
            return Results.Created($"/administrador/{administrador.Id}", new AdministradorModelView { Id = administrador.Id, Email = administrador.Email, Perfil = administrador.Perfil });
        }).RequireAuthorization("Adm").WithTags("Administradores");
    }

    private static string GerarTokenJwt(Administrador administrador, string key)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> { new Claim("Email", administrador.Email), new Claim("Perfil", administrador.Perfil), new Claim(ClaimTypes.Role, administrador.Perfil) };
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
