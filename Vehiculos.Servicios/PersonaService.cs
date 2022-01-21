using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Vehiculos.Modelos;
using Vehiculos.Servicios.Interfaces;

namespace Vehiculos.Servicios
{
    public class PersonaService : IPersonaService
    {
        private readonly HttpClient _client;

        public PersonaService()
        {
            _client = new HttpClient();
        }

        public async Task<Persona> GetPersonasAsync()
        {
            try
            {
                using var responseStream = await _client.GetStreamAsync("https://reqres.in/api/users");
                var response = await JsonSerializer.DeserializeAsync<Persona>(responseStream);

                return response;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
