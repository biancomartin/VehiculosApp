using System.ComponentModel.DataAnnotations;

namespace Vehiculos.App.Models
{
    public class Vehiculo
    {
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "La patente requiere 8 caracteres")]
        [Required]
        public string Patente { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public int Puertas { get; set; }

        [Required]
        public string Titular { get; set; }

    }
}
