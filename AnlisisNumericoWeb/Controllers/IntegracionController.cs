using Microsoft.AspNetCore.Mvc;
using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using System;
using System.Globalization;

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
            if (ModelState.IsValid)
            {
                // Lógica para calcular la integral e incluir el resultado en el modelo
                model.Resultado = TuMetodoDeIntegracion(model); // Esto es tu lógica de cálculo

                return View("Index", model); // Volver a la vista con los datos cargados
            }

            return View("Index", model); // Mostrar los errores si hay problemas
        }
        private double TuMetodoDeIntegracion(IntegracionModel model)
        {
            // Implementá acá el cálculo según el método seleccionado
            return 0.0; // Ejemplo
        }

        [HttpPost]
        public IActionResult Index(IntegracionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Validar límites (limite izquierdo < limite derecho)
                if (model.LimiteInferior >= model.LimiteSuperior)
                {
                    ModelState.AddModelError("", "El límite izquierdo debe ser menor que el derecho.");
                    return View(model);
                }

                // Evaluar la integral según método seleccionado
                switch (model.Metodo)
                {
                    case "Trapecio":
                        model.Resultado = CalcularTrapecio(model.Funcion, model.LimiteInferior, model.LimiteSuperior, model.NumeroIntervalos);
                        break;

                    case "Simpson":
                        // Subintervalos debe ser par para Simpson
                        if (model.NumeroIntervalos % 2 != 0)
                        {
                            ModelState.AddModelError("", "Para el método de Simpson, la cantidad de subintervalos debe ser un número par.");
                            return View(model);
                        }
                        model.Resultado = CalcularSimpson(model.Funcion, model.LimiteInferior, model.LimiteSuperior, model.NumeroIntervalos);
                        break;

                    default:
                        ModelState.AddModelError("", "Método de integración no soportado.");
                        return View(model);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al calcular la integral: " + ex.Message);
                return View(model);
            }
        }

        private double EvaluarFuncion(string funcion, double x)
        {
            var expr = new NCalc.Expression(funcion);
            expr.Parameters["x"] = x;
            return Convert.ToDouble(expr.Evaluate());
        }

        private double CalcularTrapecio(string funcion, double a, double b, int n)
        {
            double h = (b - a) / n;
            double suma = EvaluarFuncion(funcion, a) + EvaluarFuncion(funcion, b);

            for (int i = 1; i < n; i++)
            {
                double xi = a + i * h;
                suma += 2 * EvaluarFuncion(funcion, xi);
            }

            return (h / 2) * suma;
        }

        private double CalcularSimpson(string funcion, double a, double b, int n)
        {
            double h = (b - a) / n;
            double suma = EvaluarFuncion(funcion, a) + EvaluarFuncion(funcion, b);

            for (int i = 1; i < n; i++)
            {
                double xi = a + i * h;
                if (i % 2 == 0)
                    suma += 2 * EvaluarFuncion(funcion, xi);
                else
                    suma += 4 * EvaluarFuncion(funcion, xi);
            }

            return (h / 3) * suma;
        }
    }
}
