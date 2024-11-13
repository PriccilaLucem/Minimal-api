using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace minha_api.src.Entities;
public class Veiculo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    private int Id { get; set; } = default!;

    [Required]
    [StringLength(150)]
    private string Nome { get; set; } = default!;

    [Required]
    [StringLength(100)]
    private string Marca { get; set; } = default!;

    [Required]
    private int Ano { get; set; } = default!;

    public int GetId() => Id;
    public string GetNome() => Nome;
    public string GetMarca() => Marca;
    public int GetAno() => Ano;

    public void SetNome(string nome) => Nome = nome;
    public void SetMarca(string marca) => Marca = marca;
    public void SetAno(int ano) => Ano = ano;
    
    public Veiculo(string nome, string marca, int ano)
    {
        Nome = nome;
        Marca = marca;
        Ano = ano;
    }
}
