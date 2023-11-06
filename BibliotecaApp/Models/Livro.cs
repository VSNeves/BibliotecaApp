using BibliotecaApp.Validations;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Models
{
    public class Livro
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 255 caracteres.")]
        public string Titulo { get; set; } = default!;
        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O autor deve ter entre 3 e 100 caracteres.")]
        public string Autor { get; set; } = default!;
        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [YearRange(1700, ErrorMessage = "O ano de publicação deve ter entre 1700 e o ano atual.")]
        public int AnoPublicacao { get; set; }
        [StringLength(13)]
        public string ISBN { get; set; } = default!;
        [Range(0, 5, ErrorMessage = "A quantidade disponível deve ser entre 0 e 5 livros.")]
        public int QuantidadeDisponivel { get; set; }
        [Required(ErrorMessage = "A URL da capa é obrigatória")]
        [Url(ErrorMessage = "Forneça uma URL válida")]
        public string CapaUrl { get; set; } = string.Empty;
    }
}