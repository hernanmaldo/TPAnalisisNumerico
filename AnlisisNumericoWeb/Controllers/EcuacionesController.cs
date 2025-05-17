using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using DynamicExpresso;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Reflection;
using static AnlisisNumericoWeb.Service.EcuacionesService;

namespace AnlisisNumericoWeb.Controllers
{
    public class EcuacionesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new EcuacionesModel());
        }

        [HttpPost]
        public IActionResult Resolver(EcuacionesModel model)
        {
            try
            {
                double[] resultado;

                if (model.Metodo.ToLower() == "gauss-jordan")
                {
                    resultado = EcuacionesService.SistemasEcuaciones.GaussJordan(model.Matriz, model.VectorIndependiente);
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

                ViewBag.Solucion = resultado;
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al resolver el sistema: " + ex.Message;
            }

            return View("Index", model);
        }
    }
}
