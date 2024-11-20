using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirvisShopFinal.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public User User { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();



    }
}
