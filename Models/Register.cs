using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirvisShopFinal.Models
{
    public class Register
    {

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MinLength(2, ErrorMessage = "El nombre debe tener más de un carácter.")]
        public string name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [MinLength(2, ErrorMessage = "El apellido debe tener más de un carácter.")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 16 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?\""':{}|<>]).+$", ErrorMessage = "La contraseña debe incluir una mayúscula, una minúscula, un número y un carácter especial.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria.")]
        [Compare("password", ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }

        [NotMapped]
        public string? emailError { get; set; }

    }
}
