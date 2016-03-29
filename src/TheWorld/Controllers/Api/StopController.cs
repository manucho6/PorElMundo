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
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
   public class StopController : Controller
    {
        private CoordService _coordService;
        private ILogger<StopController> _logger;
        private ITheWorldRepository _repository;

        public StopController(ITheWorldRepository repository, ILogger<StopController> logger, CoordService coordService)

        {
        _repository=repository;
        _logger=logger;
            _coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            { 
            var results = _repository.GetTripByName(tripName,User.Identity.Name);

            if (results == null)
                {
                    return Json(null);
                }
            return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(s => s.Orden)));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Fallò al obtener las paradas de: {tripName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error al encontrar el viaje por nombre");
            }
        }

        public async  Task<JsonResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Map
                    var newStop = Mapper.Map<Stop>(vm);
                    //Geo coordenadas

                    var coordResult = await _coordService.Lookup(newStop.Nombre);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordResult.Mensaje);
                    }

                    newStop.Latitud = coordResult.Latitud;
                    newStop.Longitud = coordResult.Longitud;

                    // guardar en DB
                    _repository.AddStop(tripName,User.Identity.Name,newStop);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Fallo al guardar una nueva parada", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Fallo al guardar una nueva parada");

            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Fallò la validacion");

        }


    }
}
