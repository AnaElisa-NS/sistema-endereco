using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetoEndereco.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Usuario")]
        public string UsuarioLogin { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
