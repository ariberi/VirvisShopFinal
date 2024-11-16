using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace VirvisShopFinal.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public required string name { get; set; }
        public required string lastname { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public required Role role { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
    