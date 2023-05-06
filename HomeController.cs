using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proyecto_ED1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataStructurs.LinearStructures;
using DataStructurs.NonLinearStructures;
using DataStructurs.TreeStructures;

namespace Proyecto_ED1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static AVLTree<Pacientes> arbolPacientesAVL = new AVLTree<Pacientes>();

       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult recibirManualAVL()
        {
            return View();
        }
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string fechaProximaConsulta = collection["FechaProximaConsulta"];
                Pacientes nuevoPaciente = new Pacientes
                {
                    Name1 = collection["Nombre1"],
                    Name2 = collection["Nombre2"],
                    LName1 = collection["Apellido1"],
                    LName2 = collection["Apellido2"],
                    ID = Convert.ToInt32(collection["DPI"]),
                    age = Convert.ToInt32(collection["Edad"]),
                    Phone = Convert.ToInt32(collection["Telefono"]),
                    last_consult = Convert.ToDateTime(collection["FechaUltimaConsulta"]),
                    description = collection["Descripcion"],
                    query_format = collection["Entrada"]
                };

                return View(nuevoPaciente);
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inesperado.";
                return RedirectToAction(nameof(Index));
            }
        }



    }
}
