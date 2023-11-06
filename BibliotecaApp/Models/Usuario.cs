using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Models
{
    
    public class Usuario
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 255 caracteres.")]
        public string Nome { get; set; } = default!;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 15 caracteres.")]
        public string Senha { get; set; } = default!;

    }
}

