using System.ComponentModel.DataAnnotations;

namespace ProdutosModels
{
    public class Produto
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public required string Titulo { get; set; }
        
        public string? Descricao { get; set; }
        
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal preco { get; set; }
        
        [Required]
        [Range(0.01, int.MaxValue)]
        public int Estoque { get; set; }
        
        public List<string>? Fotos { get; set; }
        
    }
}