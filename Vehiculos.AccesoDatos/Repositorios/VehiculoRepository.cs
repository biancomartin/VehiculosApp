using System.Collections.Generic;
using System.Linq;
using Vehiculos.AccesoDatos.Repositorios.Interfaces;
using Vehiculos.Models;

namespace Vehiculos.AccesoDatos.Repositorios
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly ApplicationContext _db;

        public VehiculoRepository(ApplicationContext db)
        {
            _db = db;
        }

        public void Actualizar(Vehiculo vehiculo)
        {
            _db.Vehiculos.Update(vehiculo);
            _db.SaveChanges();
        }

        public void Agregar(Vehiculo vehiculo)
        {
            _db.Vehiculos.Add(vehiculo);
            _db.SaveChanges();
        }

        public Vehiculo ObtenerPorId(int id)
        {
            return _db.Vehiculos.FirstOrDefault(x => x.VehiculoId == id);
        }

        public List<Vehiculo> ObtenerTodos()
        {
            return _db.Vehiculos.ToList();
        }

        public void Remover(int id)
        {
            var vehiculo = ObtenerPorId(id);
            Remover(vehiculo);
        }

        public void Remover(Vehiculo vehiculo)
        {
            _db.Vehiculos.Remove(vehiculo);
            _db.SaveChanges();
        }

    }
}
