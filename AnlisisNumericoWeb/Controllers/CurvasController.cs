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
            var curvasmodel = new CurvasModel();

            return View(curvasmodel);
        }

        [HttpPost]
        public IActionResult Calcular(CurvasModel model)
        {
            try
            {
                model.Resultado = _service.Calcular(model.Puntos, model.TipoAjuste);
          
            }catch (Exception ex) 
            {
                ViewBag.Error = ex.Message;
            }

            return View("Index", model);
        }
    }
}
