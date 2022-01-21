using System.Collections.Generic;
using Vehiculos.Models;

namespace Vehiculos.AccesoDatos.Repositorios.Interfaces
{
    public interface IVehiculoRepository
    {
        Vehiculo ObtenerPorId(int id);

        List<Vehiculo> ObtenerTodos();

        void Agregar (Vehiculo vehiculo);

        void Actualizar (Vehiculo vehiculo);

        void Remover (int id);

        void Remover (Vehiculo vehiculo);
    }
}
