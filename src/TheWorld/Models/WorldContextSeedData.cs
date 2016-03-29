using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
   public class WorldContextSeedData
    {
        private WorldContext _context;
        private ILogger<WorldContextSeedData> _logger;
        private UserManager<WorldUser> _userManager;

        public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager, ILogger<WorldContextSeedData> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;

        }
            public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("manu@theworld.com")== null)
            {
                //add user
                var newUser = new WorldUser()
                {
                    UserName = "manucho6",
                    Email = "manu@theworld.com"
                };
                try
                { 
                await _userManager.CreateAsync(newUser, "Playadito1!");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Fallo el alta de usuario", ex);
                }
            }
                if (!_context.Trips.Any())
            {

                var usTrip = new Trip()
                {
                    Nombre = "US trip",
                    Creacion = DateTime.UtcNow,
                    Usuario = "manucho6",
                    Stops = new List<Stop>()
                    {
                    new Stop() {Nombre = "Atlanta, GA", Arrivo = new DateTime(2014, 6, 4), Latitud = 33.748995, Longitud = -84.387982, Orden = 0 },
                    new Stop() {Nombre = "New York, NY", Arrivo = new DateTime(2014, 6, 9), Latitud = 40.712784, Longitud= -74.005941, Orden = 1 },
                    new Stop() {Nombre = "Boston, MA", Arrivo = new DateTime(2014, 7, 1), Latitud = 42.360084, Longitud = -71.058880, Orden = 2 },
                    new Stop() {Nombre = "Chicago, IL", Arrivo = new DateTime(2014, 7, 10), Latitud = 41.878114, Longitud = -87.629798, Orden = 3 },
                    new Stop() {Nombre = "Seattle, WA", Arrivo = new DateTime(2014, 8, 13), Latitud = 47.606209, Longitud = -122.332071, Orden = 4 },
                    new Stop() {Nombre = "Atlanta, GA", Arrivo = new DateTime(2014, 8, 23), Latitud = 33.748995, Longitud = -84.387982, Orden = 0 }
                    }

                };

                _context.Trips.Add(usTrip);
                _context.Stops.AddRange(usTrip.Stops);
         
            var worldTrip = new Trip()
            {
                Nombre = "World Trip",
                Creacion = DateTime.UtcNow,
                Usuario = "manucho6",
                Stops = new List<Stop>()
                {
                    new Stop() {Orden = 0, Latitud = 33.748995, Longitud = -84.387982,Nombre = "Atlanta, Georgia", Arrivo = DateTime.Parse("Jun 3, 2014")},
                    new Stop() {Orden = 1, Latitud = 48.856614, Longitud = 2.352222, Nombre = "Paris, Francia", Arrivo = DateTime.Parse("Jun 4, 2014")},
                    new Stop() {Orden = 2, Latitud = 50.850000, Longitud = 4.350000,Nombre = "Bruselas, Belgica", Arrivo = DateTime.Parse("Jun 25, 2014")},
                    new Stop() {Orden = 3, Latitud = 51.209348, Longitud = 3.224700,Nombre = "Bruges, Belgica", Arrivo = DateTime.Parse("Jun 28, 2014")},
                    new Stop() {Orden = 4, Latitud = 48.856614, Longitud = -84.387982,Nombre = "Paris, Francia", Arrivo = DateTime.Parse("Jun 30, 2014")},
                    new Stop() {Orden = 5, Latitud = 51.508515, Longitud = -0.125487,Nombre = "Londres, UK", Arrivo = DateTime.Parse("Jul 8, 2014")},
                    new Stop() {Orden = 6, Latitud = 51.454513, Longitud = -2.587910,Nombre = "Bristol, UK", Arrivo = DateTime.Parse("Jun 24, 2014")},
                }
            };
            _context.Trips.Add(worldTrip);
            _context.Stops.AddRange(worldTrip.Stops);

                _context.SaveChanges();

            };

        }
    }
}
