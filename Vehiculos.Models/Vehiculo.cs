using System.ComponentModel.DataAnnotations;

namespace Vehiculos.Models
{
    public class Vehiculo
    {
        [Key]
        public int VehiculoId { get; set; }

        public string Patente { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public int Puertas { get; set; }

        public int TitularId { get; set; }
    }
}
