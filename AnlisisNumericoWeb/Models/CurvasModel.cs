using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace AnlisisNumericoWeb.Models
{
    public class CurvasModel
    {
        public List<Punto> Puntos { get; set; } = new();
        public CurvaResult Resultado { get; set; } = new();
        public string TipoAjuste { get; set; } = "Lineal";
    }
}
