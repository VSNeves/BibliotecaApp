using System.ComponentModel.DataAnnotations;

namespace BibliotecaApp.Models
{
    public class Cliente : Usuario
    {
        public required string Endereco { get; set; }
        public required string Telefone { get; set; }
    }
}
