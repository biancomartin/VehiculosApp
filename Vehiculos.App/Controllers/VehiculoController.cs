using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehiculos.AccesoDatos;
using Vehiculos.App.Models;
using Vehiculos.Modelos;
using Vehiculos.Modelos.Dto;
using Vehiculos.Models;
using Vehiculos.Servicios.Interfaces;

namespace Vehiculos.App.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IPersonaService _personaService;
        private readonly IMapper _mapper;

        public VehiculoController(ApplicationContext context, IPersonaService personaService, IMapper mapper)
        {
            _context = context;
            _personaService = personaService;
            _mapper = mapper;
        }

        // GET: Vehiculo
        public async Task<IActionResult> Index()
        {
            var personasDto = await GetPersonaDtos();
            var vehiculosDB = await _context.Vehiculos.ToListAsync();
            var vehiculos = _mapper.Map<IEnumerable<VehiculoViewModel>>(vehiculosDB).ToList();
            vehiculos.ForEach(x => x.Titular = personasDto.FirstOrDefault(y => y.Value == x.TitularId.ToString()).Text);

            return View(vehiculos);
        }

        // GET: Vehiculo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .FirstOrDefaultAsync(m => m.VehiculoId == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            var personasDto = await GetPersonaDtos();
            var vehiculoViewModel = _mapper.Map<VehiculoViewModel>(vehiculo);
            vehiculoViewModel.Titular = personasDto.FirstOrDefault(y => y.Value == vehiculo.TitularId.ToString()).Text;

            return View(vehiculoViewModel);
        }

        // GET: Vehiculo/Create
        public async Task<IActionResult> Create()
        {
            var vehiculo = new VehiculoCreateViewModel();
            var personasDto = await GetPersonaDtos();

            vehiculo.TitularLista = personasDto;
            return View(vehiculo);
        }

        // POST: Vehiculo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehiculoId,Patente,Marca,Modelo,Puertas,TitularId")] VehiculoCreateViewModel vehiculoViewModel)
        {
            var vehiculo = _mapper.Map<Vehiculo>(vehiculoViewModel);
            if (ModelState.IsValid)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        // GET: Vehiculo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculoDb = await _context.Vehiculos.FindAsync(id);
            if (vehiculoDb == null)
            {
                return NotFound();
            }

            var vehiculo = _mapper.Map<VehiculoCreateViewModel>(vehiculoDb);
            var personasDto = await GetPersonaDtos();

            // Create a viewmodel, and create the dropdown
            vehiculo.TitularLista = personasDto;
            vehiculo.TitularId = vehiculoDb.TitularId;
            return View(vehiculo);
        }

        // POST: Vehiculo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehiculoId,Patente,Marca,Modelo,Puertas,TitularId")] VehiculoCreateViewModel vehiculoViewModel)
        {
            var vehiculo = _mapper.Map<Vehiculo>(vehiculoViewModel);
            if (id != vehiculo.VehiculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.VehiculoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        // GET: Vehiculo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculoDb = await _context.Vehiculos
                .FirstOrDefaultAsync(m => m.VehiculoId == id);
            if (vehiculoDb == null)
            {
                return NotFound();
            }

            var vehiculoViewModel = _mapper.Map<VehiculoViewModel>(vehiculoDb);
            var personasDto = await GetPersonaDtos();
            vehiculoViewModel.Titular = personasDto.FirstOrDefault(y => y.Value == vehiculoDb.TitularId.ToString()).Text;

            return View(vehiculoViewModel);
        }

        // POST: Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.VehiculoId == id);
        }

        private async Task<IEnumerable<SelectListItem>> GetPersonaDtos()
        {
            var personasFromApi = await _personaService.GetPersonasAsync();

            var personas = new List<Datos>();

            var datos = personasFromApi.Select(x => x.data);
            datos.ToList().ForEach(x => personas.AddRange(x));

            var personasDto = _mapper.Map<IEnumerable<PersonaDto>>(personas);
            return personasDto.Select(i => new SelectListItem()
            {
                Text = i.NombreCompleto,
                Value = i.Id.ToString()
            });
        }

    }
}
