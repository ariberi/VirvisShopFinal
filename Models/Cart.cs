namespace VirvisShopFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Cart
{
    [Key]
    public int CartId { get; set; }


    [Required]
    public string Status { get; set; } // Active, Completed, etc.

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<CartItem> CartItems { get; set; }

    public User User { get; set; }
}
