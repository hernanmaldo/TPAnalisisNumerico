using Microsoft.AspNetCore.Mvc;
using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using System;
using System.Globalization;
using NCalc;

namespace AnlisisNumericoWeb.Controllers
{
    public class IntegracionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Resultado(IntegracionModel model)
        {
           
                model.Resultado = CalcularIntegral(model);
           

            return View("Resultado", model); // Asegurate de tener una vista Resultado.cshtml
        }
        private double EvaluarFuncion(string funcion, double x)
        {
            var expr = new Expression(funcion);
            expr.Parameters["x"] = x;
            return Convert.ToDouble(expr.Evaluate());
        }

        private double CalcularIntegral(IntegracionModel model)
        {
            var service = new IntegracionService();

            var result =   service.Calcular(model);
            model.Resultado = result;  

            return result;  
        }

    }
}

