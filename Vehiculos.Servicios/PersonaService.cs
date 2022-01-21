using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Persona>> GetPersonasAsync()
        {
            try
            {
                int pages = _configuration.GetValue<int>("TitularesAPI:PagesToCheck");
                List<Persona> response = new List<Persona>();
                for (int i=1; i<= pages; i++)
                {
                    using var responseStream = await _client.GetStreamAsync(_configuration.GetValue<string>("TitularesAPI:URLBase") + $"?page={i}");
                    var personas = await JsonSerializer.DeserializeAsync<Persona>(responseStream);
                    if (personas.data.Any())
                    {
                        response.Add(personas);
                    }
                }

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
