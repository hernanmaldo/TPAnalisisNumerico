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
            return View(new IntegracionModel());
        }

        [HttpPost]
        public IActionResult Resultado(IntegracionModel model)
        {
            double resultado = CalcularIntegral(model);
            model.Resultado = resultado;
            return View(model);
        }

        private double EvaluarFuncion(string funcion, double x)
        {
            var expr = new Expression(funcion);
            expr.Parameters["x"] = x;
            return Convert.ToDouble(expr.Evaluate());
        }

        private double CalcularIntegral(IntegracionModel model)
        {
            double a = model.LimiteInferior;
            double b = model.LimiteSuperior;
            int n = model.NumeroIntervalos;
            double h = (b - a) / n;

            switch (model.Metodo)
            {
                case "TrapecioSimple":
                    return (EvaluarFuncion(model.Funcion, a) + EvaluarFuncion(model.Funcion, b)) * (b - a) / 2;

                case "TrapecioMultiple":
                    double sum = 0;
                    for (int i = 1; i < n; i++)
                    {
                        sum += EvaluarFuncion(model.Funcion, a + i * h);
                    }
                    return (h / 2) * (EvaluarFuncion(model.Funcion, a) + 2 * sum + EvaluarFuncion(model.Funcion, b));

                // Agregá los otros métodos si necesitás

                default:
                    return 0;
            }
        }
    }
}
