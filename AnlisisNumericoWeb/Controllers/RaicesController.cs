using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using DynamicExpresso;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Reflection;


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

            Debug.WriteLine("Pete");
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


            Func<double, double> funcion; 
            try
            {
                funcion = ParseFunc(func);

            }catch(Exception ex)
            {
                ViewBag.Error = "Ocurrió un error: Funcion incorrecta";

                return View(metodo, model);
            }
      
            try
            {
                var raiz = RaicesService.BuscarRaiz(funcion, metodo,model);

                model.Raiz = raiz;

                return View(metodo, model);
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Ocurrió un error: " + ex.Message;
                return View(metodo,model);
            }

      
        }
    


        public Func<double, double> ParseFunc(string func)
        {
            var interpreter = new Interpreter();
   
            var function = interpreter.ParseAsDelegate<Func<double, double>>(func, "x");
            //Debug.WriteLine(function(5));
            return function;
        }   


}
}
