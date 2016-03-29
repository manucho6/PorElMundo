using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailservice;
        private ITheWorldRepository _repository;

        public AppController(IMailService service, ITheWorldRepository repository)
        {
            _mailservice = service;
            _repository = repository;
        }
        public IActionResult Index()
        {
           
            return View();
        }
        [Authorize]
        public IActionResult Trips()
        {
          return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            { 
            var email = Startup.Configuration["AppSettings:SiteEmailAddress"];
            if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.AddModelError("", "No se puedo enviar el mail.");
                }
             if(  _mailservice.SendMail(email, email, $"Contact Page from {model.Nombre} ({model.Email})",model.Mensaje))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Enviado. Muchas gracias!";
                }
            }

            return View();
        }
    }
}
