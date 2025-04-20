using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using DynamicExpresso;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace AnlisisNumericoWeb.Controllers
{
    public class RaicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Biseccion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReglaFalsa()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Tangente()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Secante()
        {
            return View();
        }


        [HttpPost("Calcular")]
        public IActionResult Calcular(string metodo, RaizData model)
        {
            if (!ModelState.IsValid)
            {


                return View(metodo, model);
            }
            var func = model.Funcion.Replace("f(x) = ", "").Replace(" ", "");

            var funcion = ParseFunc(func);
            Console.WriteLine(funcion.ToString());

            var raiz = RaicesService.BuscarRaiz(funcion, metodo, model.Iteraciones, model.Tolerancia, model.Xi, model.Xd);

            model.Raiz = raiz;

            return View(metodo, model);
        }
    


        public Func<double, double> ParseFunc(string func)
        {
            var interpreter = new Interpreter();
   
            var function = interpreter.ParseAsDelegate<Func<double, double>>(func, "x");
            Debug.WriteLine(function(5));
            return function;
        }   


}
}
