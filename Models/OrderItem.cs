using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirvisShopFinal.Models
{
    public class OrderItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int productId { get; set; }
        [ForeignKey("productId")]
        public Product Product { get; set; }

        [Required]
        public int orderId { get; set; }
        [ForeignKey("orderId")]
        public Order Order { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public double subtotal { get; set; }

    }
}
