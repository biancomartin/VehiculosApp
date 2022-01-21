using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger<PersonaService> _logger;

        public PersonaService(IConfiguration configuration, ILogger<PersonaService> logger)
        {
            _client = new HttpClient();
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Persona> GetPersonasAsync()
        {
            try
            {
                using var responseStream = await _client.GetStreamAsync(_configuration.GetValue<string>("TitularesAPI:URLBase"));
                var response = await JsonSerializer.DeserializeAsync<Persona>(responseStream);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al recuperar la lista de personas");
                return null;
            }
        }
    }
}
