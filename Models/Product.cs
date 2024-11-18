using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirvisShopFinal.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public required string name { get; set; }
        public required double price { get; set; }
        public required string description { get; set; }
        public required string img { get; set; }
        public required double descuento { get; set; }
        public required Category category { get; set; }


        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
