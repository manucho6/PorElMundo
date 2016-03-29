using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace TheWorld.Services
{
    public class CoordService
    {
        private ILogger<CoordService> _logger;

        public CoordService(ILogger<CoordService> logger)
        {
            _logger = logger;
        }

        public async Task<CoordServiceResult> Lookup (string location)
        {
            var result = new CoordServiceResult()
            {
                Success = false,
                Mensaje = "No se pudo determinar las coordenadas"
            };

            // buscar coord
            var bingkey = Startup.Configuration["BingKey"];
            var encodedName = WebUtility.UrlEncode(location);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations/{encodedName}?l&key={bingkey}";

            var client = new HttpClient();

            var json= await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Mensaje = $"No se pudo encontrar: {location}";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence!="High")
                {
                    result.Mensaje = $"No se pudo encontra una coincidencia confidente para: {location}";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Longitud = (double)coords[1];
                    result.Latitud = (double)coords[0];
                    result.Success = true;
                    result.Mensaje = "Encontrado!!";
                }
            }
           


            return result;
        }
    }
}
