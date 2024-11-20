using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirvisShopFinal.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public User User { get; set; }

        [Required]
        public double total { get; set; }

        [Required]
        public OrderStatus status { get; set; } = OrderStatus.Pending;

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;


        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();


    }
}
