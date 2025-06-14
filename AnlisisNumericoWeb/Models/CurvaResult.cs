using System.ComponentModel.DataAnnotations;

namespace AnlisisNumericoWeb.Models
{
    public class CurvaResult
    {
        public string Funcion { get; set; }
        public double[] Coeficientes { get; set; }
        public string Tipo { get; set; }
    }
}
