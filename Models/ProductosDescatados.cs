using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace VirvisShopFinal.Models
{
    public class ProductosDescatados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int codDestacado { get; set; }

        public required Product Product{ get; set; }

    }
}
