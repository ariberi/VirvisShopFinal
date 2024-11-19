using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirvisShopFinal.Models
{
    public class Review
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "El comentario no puede exceder los 500 caracteres.")]
        public string comment { get; set; } = string.Empty;

        [Required]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        public int rating { get; set; }

        [Required]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public User User { get; set; }

        [Required]
        public int productId { get; set; }
        [ForeignKey("productId")]
        public Product Product { get; set; }
    }
}