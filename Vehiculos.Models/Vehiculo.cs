using System.ComponentModel.DataAnnotations;


namespace Vehiculos.Models
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }

        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "La patente requiere 8 caracteres")]
        [Required(ErrorMessage = "La patente es obligatoria en cada vehiculo")]
        public string Patente { get; set; }

        [Required(ErrorMessage = "Especifique la marca del vehiculo")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Indique el modelo del vehiculo")]
        public string Modelo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingrese un numero mayor a {1}")]
        [Required(ErrorMessage = "Indique el numero de puertas")]
        public int Puertas { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del propietario del auto")]
        public string Titular { get; set; }

    }
}
