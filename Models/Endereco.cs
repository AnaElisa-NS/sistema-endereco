using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoEndereco.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required]
        public string? Cep { get; set; }

        [Required]
        public string? Logradouro { get; set; }

        public string? Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Uf { get; set; }

        [Required]
        public string Numero { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
