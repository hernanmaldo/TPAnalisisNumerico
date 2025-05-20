using Microsoft.AspNetCore.Mvc;
using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using System;
using System.Globalization;

namespace AnlisisNumericoWeb.Controllers
{
    public class EcuacionesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Inicializa una matriz 3x3 con ceros y un vector independiente con ceros
            var model = new EcuacionesModel
            {
                Matriz = Enumerable.Range(0, 4)
                                   .Select(_ => Enumerable.Repeat(0.0, 4).ToList())
                                   .ToList(),
                VectorIndependiente = Enumerable.Repeat(0.0, 4).ToList(),
                Incognitas = Enumerable.Repeat(0.0, 4).ToList()
            };

            return View("GaussJordan", model);
        }

        [HttpPost]
        public IActionResult Ajustar(int size)
        {
            var model = new EcuacionesModel
            {
                Matriz = Enumerable.Range(0, size)
                       .Select(_ => Enumerable.Repeat(0.0, size).ToList())
                       .ToList(),
                VectorIndependiente = Enumerable.Repeat(0.0, size).ToList(),
                Incognitas = Enumerable.Repeat(0.0, size).ToList()
            };

            return View("GaussJordan", model);
        }

        [HttpPost]
        public IActionResult Resolver(EcuacionesModel model)
        {
            try
            {
                if (model.Matriz == null || model.VectorIndependiente == null)
                    throw new Exception("Datos incompletos");

                if (model.Matriz.Count != model.VectorIndependiente.Count)
                    throw new Exception("La cantidad de filas de la matriz y del vector no coincide");

                List<double> resultado;

                if (model.Metodo.ToLower() == "gauss-jordan")
                {
                    resultado = EcuacionesService.SistemasEcuaciones.GaussJordan(
                        model.Matriz,
                        model.VectorIndependiente
                    );
                }
                else if (model.Metodo.ToLower() == "gauss-seidel")
                {
                    resultado = EcuacionesService.SistemasEcuaciones.GaussSeidel(
                        model.Matriz,
                        model.VectorIndependiente,
                        model.Tolerancia,
                        model.Iteraciones
                    );
                }
                else
                {
                    throw new Exception("Método no válido.");
                }

                model.Incognitas = resultado;
                ViewBag.Solucion = resultado;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al resolver el sistema: " + ex.Message;
            }

            return View("GaussJordan", model);
        }
    }
}
