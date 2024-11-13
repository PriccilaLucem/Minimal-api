using minha_api.src.Entities;
using minha_api.src.Dto;

namespace minha_api.src.Interfaces;
public interface IAdministradorServico
{    Administrador? Login(LoginDTO loginDTO);
    Administrador Incluir(Administrador administrador);
    Administrador? BuscaPorId(int id);
    List<Administrador> Todos(int? pagina);
}