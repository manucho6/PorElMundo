using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class TheWorldRepository : ITheWorldRepository
    {
        private WorldContext _context;
        private ILogger<TheWorldRepository> _logger;

        public TheWorldRepository(WorldContext context, ILogger<TheWorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddStop(string tripName,string username , Stop newStop)
        {
            var theTrip = GetTripByName(tripName, username);
            if (theTrip.Stops.Any()){ 
            newStop.Orden = theTrip.Stops.Max(s => s.Orden) + 1;
            }
            else
            {
                newStop.Orden = 0;
            };
            theTrip.Stops.Add(newStop);
            _context.Stops.Add(newStop);
        }

        public void AddTrip(Trip newTrip)
        {
            _context.Add(newTrip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Nombre).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("No se pudo obtener los viajes de la base de datas", ex);
                return null;
            }
        }
        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips
              .Include(t => t.Stops)
              .OrderBy(t => t.Nombre)
              .ToList();
            }
          catch (Exception ex)
            {
                _logger.LogError("No se pudo obtener los viajes con paradas de la base de datos", ex);
                return null;
            }
        }

        public Trip GetTripByName(string tripName,string username)
        {
            return _context.Trips.Include(t => t.Stops)
                .Where(t => t.Nombre == tripName && t.Usuario==username)
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetUserAllTripsWithStops(string name)
        {
            try
            {
                return _context.Trips
              .Include(t => t.Stops)
              .OrderBy(t => t.Nombre)
              .Where(t=> t.Usuario==name)
              .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("No se pudo obtener los viajes con paradas de la base de datos", ex);
                return null;
            }
        }
            public bool SaveAll()
        {
           return  _context.SaveChanges() > 0;
        }
    }
}
