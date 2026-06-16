using System.ComponentModel.DataAnnotations;

namespace EcommerceModa.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        [StringLength(500)]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, 99999.99, ErrorMessage = "O preço deve ser maior que zero.")]
        [Display(Name = "Preço (R$)")]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, 10000, ErrorMessage = "O estoque deve ser entre 0 e 10000.")]
        [Display(Name = "Quantidade em Estoque")]
        public int Estoque { get; set; }

        [Display(Name = "Tamanhos Disponíveis")]
        public string? Tamanhos { get; set; }

        [Display(Name = "Cor")]
        public string? Cor { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Categorias disponíveis
        public static readonly string[] Categorias = {
            "Vestidos", "Blusas", "Calças", "Saias", "Casacos",
            "Acessórios", "Calçados", "Íntimos", "Moda Praia", "Outros"
        };
    }
}
