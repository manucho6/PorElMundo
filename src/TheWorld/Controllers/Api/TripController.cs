using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api

{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private ITheWorldRepository _repository;

        public TripController(ITheWorldRepository repository, ILogger<TripController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = _repository.GetUserAllTripsWithStops(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<TripViewModel>>(trips);
            return Json(results);
        }


        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {

            
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(vm);
                    newTrip.Usuario = User.Identity.Name;

                
                _logger.LogInformation("Tratando de guardar un nuevo viaje");
                    _repository.AddTrip(newTrip);

                if (_repository.SaveAll())
                { 
                    Response.StatusCode = (int) HttpStatusCode.Created;
                    return Json(Mapper.Map<TripViewModel>(newTrip));
                }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Fallò al guardar el nuevo viaje", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Falló", ModelState= ModelState });

        }
    }
}
