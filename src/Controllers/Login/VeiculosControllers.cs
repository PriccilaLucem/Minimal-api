// VeiculosEndpoints.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using minha_api.src.Interfaces;
using minha_api.src.Entities;
using minha_api.src.Dto;

public static class VeiculosEndpoints
{
    public static void MapVeiculoEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/veiculos", ([FromBody] VeiculoDTO veiculoDTO, IVeiculoServico veiculoServico) =>
        {
            if (string.IsNullOrEmpty(veiculoDTO.Nome) || string.IsNullOrEmpty(veiculoDTO.Marca) || veiculoDTO.Ano < 1950)
                return Results.BadRequest(new { Message = "Preencha todos os campos corretamente." });

            var veiculo = new Veiculo { Nome = veiculoDTO.Nome, Marca = veiculoDTO.Marca, Ano = veiculoDTO.Ano };
            veiculoServico.Incluir(veiculo);
            return Results.Created($"/veiculo/{veiculo.Id}", veiculo);
        }).RequireAuthorization("Adm,Editor").WithTags("Veiculos");

        endpoints.MapGet("/veiculos", ([FromQuery] int? pagina, IVeiculoServico veiculoServico) =>
        {
            var veiculos = veiculoServico.Todos(pagina);
            return Results.Ok(veiculos);
        }).RequireAuthorization().WithTags("Veiculos");

        endpoints.MapGet("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
        {
            var veiculo = veiculoServico.BuscaPorId(id);
            if (veiculo == null) return Results.NotFound();
            return Results.Ok(veiculo);
        }).RequireAuthorization("Adm,Editor").WithTags("Veiculos");

        endpoints.MapDelete("/veiculos/{id}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
        {
            var veiculo = veiculoServico.BuscaPorId(id);
            if (veiculo == null) return Results.NotFound();
            veiculoServico.Apagar(veiculo);
            return Results.NoContent();
        }).RequireAuthorization("Adm").WithTags("Veiculos");
    }
}
