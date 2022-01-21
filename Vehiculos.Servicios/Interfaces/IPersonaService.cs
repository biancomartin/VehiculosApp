using System.Collections.Generic;
using System.Threading.Tasks;
using Vehiculos.Modelos;

namespace Vehiculos.Servicios.Interfaces
{
    public interface IPersonaService
    {
        Task<Persona> GetPersonasAsync();
    }
}
