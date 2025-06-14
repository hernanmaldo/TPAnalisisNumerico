using AnlisisNumericoWeb.Models;
using AnlisisNumericoWeb.Service;
using DynamicExpresso;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Reflection;

namespace AnlisisNumericoWeb.Controllers
{
    public class CurvasController : Controller
    {
        private readonly CurvasService _service = new();

        public IActionResult Index()
        {
            return View(new CurvasModel());
        }

        [HttpPost]
        public IActionResult Calcular(CurvasModel model)
        {
            model.Resultado = _service.Calcular(model.Puntos, model.TipoAjuste);
            return View("Resultado", model);
        }
    }
}
