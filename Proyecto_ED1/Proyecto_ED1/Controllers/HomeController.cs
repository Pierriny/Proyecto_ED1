using Proyecto_ED1.Models;
using DataStructurs.NonLinearStructures;
using DataStructurs.LinearStructures;
using DataStructurs.TreeStructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using CsvHelper;
using System.IO;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;

namespace Proyecto_ED1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static AVLTree<Pacientes> arbolPacientesAVL = new AVLTree<Pacientes>();


        Stopwatch stopwatch = new Stopwatch();

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


        //String name1, String name2, String Lname1, String Lname2, long id, int age, int phone, int last, int next, String description

        public IActionResult guardarPacientesAVL(String _name1, String _name2, String _Lname1, String _Lname2, long _id, int _age, int _phone, int _last, int _next, String _description, String _asistencia)
        {
            DateTime timee = new DateTime();
            Pacientes nuevoPaciente = new Pacientes(_name1,  _name2, _Lname1,  _Lname2,  _id, _age, _phone, _last, _next, _description, _asistencia);
            arbolPacientesAVL.Add(nuevoPaciente, new Models.Comparadores.ComparerDPI());
            timee = DateTime.Now;
            if (arbolPacientesAVL.getRotaciones() != 1)
            {
                ViewBag.rotaciones = "Se realizaron " + arbolPacientesAVL.getRotaciones() + " rotaciones.";
                ViewBag.fecha = "Fecha y hora a la que se guardo el registro del paciente: " + timee;
            }
                
            else
            {
                ViewBag.rotaciones = "Se realizó " + arbolPacientesAVL.getRotaciones() + " rotación.";
                ViewBag.fecha = "Fecha y hora a la que se guardo el registro del paciente: " + timee;
            }       
            return View();
        }


        public IActionResult buscarCitas(int dia)
        {
            Pacientes buscarCitas = new Pacientes();
            buscarCitas.next_consult = dia;
            Recorrido<Pacientes> nuevoRecorrido = new Recorrido<Pacientes>();
            arbolPacientesAVL.InOrder(nuevoRecorrido);
            AVLTree<Pacientes> arbolBuscarIDAVL = new AVLTree<Pacientes>();
            foreach (var item in nuevoRecorrido.arbolEnLista as List<Pacientes>)
            {
                arbolBuscarIDAVL.Add(item, new Models.Comparadores.ComparerNextConsulta());
            }
            if (arbolBuscarIDAVL.Contains(buscarCitas, new Models.Comparadores.ComparerNextConsulta()))
            {
                Pacientes encontrado = arbolBuscarIDAVL.Find(buscarCitas, new Models.Comparadores.ComparerNextConsulta());
                ViewBag.citasssssFound = "El dia seleccionado tiene las siguientes citas " + encontrado.ID ;
                ViewBag.comparaciones = "Se realizaron " + arbolBuscarIDAVL.getComparaciones() + " comparaciones.";
            }
            else
            {
                ViewBag.NextConsulta = "No hay citas agendadas ese dia";
                ViewBag.comparaciones = "";
            }
            return View();
        }



        public IActionResult buscarID(int _id)
        {
            Pacientes buscarID = new Pacientes();
            buscarID.ID = _id;
            Recorrido<Pacientes> nuevoRecorrido = new Recorrido<Pacientes>();
            arbolPacientesAVL.InOrder(nuevoRecorrido);
            AVLTree<Pacientes> arbolBuscarIDAVL = new AVLTree<Pacientes>();
            foreach (var item in nuevoRecorrido.arbolEnLista as List<Pacientes>)
            {
                arbolBuscarIDAVL.Add(item, new Models.Comparadores.ComparerDPI());
            }
            if (arbolBuscarIDAVL.Contains(buscarID, new Models.Comparadores.ComparerDPI()))
            {
                Pacientes encontrado = arbolBuscarIDAVL.Find(buscarID, new Models.Comparadores.ComparerDPI());
                ViewBag.PacienteFound = "El Paciente con el DPI/ID " + encontrado.ID + " esta registrado en el sistema ";
                ViewBag.comparaciones = "Se realizaron " + arbolBuscarIDAVL.getComparaciones() + " comparaciones.";
            }
            else
            {
                ViewBag.PacienteFound = "No se encontró un paciente con tal DPI/ID.";
                ViewBag.comparaciones = "";
            }
            return View();
        }


        public IActionResult buscarNombre(String _name1)
        {
            Pacientes buscarN = new Pacientes();
            buscarN.Name1 = _name1;

            Recorrido<Pacientes> nuevoRecorrido = new Recorrido<Pacientes>();
            arbolPacientesAVL.InOrder(nuevoRecorrido);
            AVLTree<Pacientes> arbolBuscarSerieAVL = new AVLTree<Pacientes>();
            foreach (var item in nuevoRecorrido.arbolEnLista as List<Pacientes>)
            {
                arbolBuscarSerieAVL.Add(item, new Models.Comparadores.comparerNombre());
            }
            if (arbolBuscarSerieAVL.Contains(buscarN, new Models.Comparadores.comparerNombre()))
            {
                Pacientes encontrado = arbolBuscarSerieAVL.Find(buscarN, new Models.Comparadores.comparerNombre());
                ViewBag.PacienteFound = "El Paciente de nombre " + encontrado.Name1 + " " + encontrado.LName1 + " esta registrado en el Sistema ";
                ViewBag.comparaciones = "Se realizaron " + arbolBuscarSerieAVL.getComparaciones() + " comparaciones.";
            }
            else
            {
                ViewBag.PacienteFound = "No se encontró al Paciente que esta buscando.";
                ViewBag.comparaciones = "";
            }
            return View();
        }


        public IActionResult mostrarArbolAVL(int orden)
        {
            Recorrido<Pacientes> nuevoRecorrido = new Recorrido<Pacientes>();
            arbolPacientesAVL.InOrder(nuevoRecorrido);
            arbolPacientesAVL = null;
            DateTime time = new DateTime();
            DateTime time2 = new DateTime();
            DateTime time3 = new DateTime();
            DateTime time4 = new DateTime();
            GC.Collect();
            if (orden == 1)
            {
                stopwatch.Reset();
                stopwatch.Start();

                arbolPacientesAVL = new AVLTree<Pacientes>();
                foreach (var item in nuevoRecorrido.arbolEnLista as List<Pacientes>)
                {
                    arbolPacientesAVL.Add(item, new Models.Comparadores.comparerNombre());
                }
                nuevoRecorrido = null;
                stopwatch.Stop();
                var tiempo = stopwatch.ElapsedMilliseconds;
                time2 = DateTime.Now;

                ViewBag.tiempo = "Tardó " + tiempo + " ms o " + stopwatch.ElapsedTicks + " ticks en ordenarse.";
                ViewBag.Hora = "Fecha y hora que se hizo el ordenamiento: " + time2;
            }
            else if (orden == 2)
            {
                stopwatch.Reset();
                stopwatch.Start();
                arbolPacientesAVL = new AVLTree<Pacientes>();
                foreach (var item in nuevoRecorrido.arbolEnLista as List<Pacientes>)
                {
                    arbolPacientesAVL.Add(item, new Models.Comparadores.ComparerDPI());
                }
                nuevoRecorrido = null;
                stopwatch.Stop();
                var tiempo = stopwatch.ElapsedMilliseconds;
                time3 = DateTime.Now;

                ViewBag.tiempo = "Tardó " + tiempo + " ms o " + stopwatch.ElapsedTicks + " ticks en ordenarse.";
                ViewBag.Hora = "Fecha y hora que se hizo el ordenamiento: " + time3;
            }
            else
            {
                stopwatch.Reset();
                stopwatch.Start();
                arbolPacientesAVL = new AVLTree<Pacientes>();

                foreach (var item in nuevoRecorrido.arbolEnLista as List<Pacientes>)
                {
                    arbolPacientesAVL.Add(item, new Models.Comparadores.ComparerDPIfiltrados());
                }
                nuevoRecorrido = null;
                stopwatch.Stop();
                var tiempo = stopwatch.ElapsedMilliseconds;
                time4 = DateTime.Now;
                ViewBag.tiempo = "Tardó " + tiempo + " ms o " + stopwatch.ElapsedTicks + " ticks en ordenarse.";
                ViewBag.Hora = "Fecha y hora a la que se aplico seguimiento a los pacientes: " + time4;
            }

            time = DateTime.Now;
            GC.Collect();
            nuevoRecorrido = new Recorrido<Pacientes>();
            arbolPacientesAVL.InOrder(nuevoRecorrido);
            ViewBag.Altura = "El árbol tiene una altura de " + arbolPacientesAVL.getAltura();
            ViewBag.Nodes = "El árbol tiene  " + arbolPacientesAVL.getNodes() + " elementos.";
            ViewBag.FyH = "Fecha y hora a la que se accedio al registro de pacientes: " + time;
            ViewData["Pacientes"] = nuevoRecorrido.arbolEnLista;
            return View();
        }

        public IActionResult Dia() { return View(); }






    }
}
