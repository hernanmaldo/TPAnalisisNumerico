using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using DynamicExpresso;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AnlisisNumericoWeb.Controllers
{
    public class RaicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calcular(RaizData model)
        {
            if (ModelState.IsValid)
            {
                var funcion1 = model.Funcion.Replace("f(x) = ", "");

                var funcion = ParseFunc(funcion1);
                Console.WriteLine(funcion.ToString());

                var raiz = RaicesService.CalcularXr("bisección", funcion , model.Xi, model.Xd);

                 model.Raiz = raiz;   

                return View("Index", model);
            }

            return View(model); 

        }


        public Func<double, double> ParseFunc(string func)
        {
            var interpreter = new Interpreter();

        
            var function = interpreter.ParseAsDelegate<Func<double, double>>(func, "x");

            return function;
        }


}
}
