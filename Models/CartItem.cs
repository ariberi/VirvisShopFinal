using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirvisShopFinal.Models
{
    public class CartItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int productId { get; set; }
        [ForeignKey("productId")]
        public Product Product { get; set; }

        [Required]
        public int cartId { get; set; }
        [ForeignKey("cartId")]
        public Cart Cart { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public double price { get; set; }

    }
}
