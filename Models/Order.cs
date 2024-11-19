namespace VirvisShopFinal.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int OrderId { get; set; }



    [Required]
    public int CartId { get; set; }

    [Required]
    public decimal Total { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public Cart Cart { get; set; }
}
