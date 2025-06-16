using System.ComponentModel.DataAnnotations;

namespace AnlisisNumericoWeb.Models
{
    public class IntegracionModel
    {
        public string Funcion { get; set; } = string.Empty;

        public double LimiteInferior { get; set; }
        public double LimiteSuperior { get; set; }
        public int NumeroIntervalos { get; set; }

        public string Metodo { get; set; } = "trapecio"; // por defecto
        public double? Resultado { get; set; }

        // Para graficar
        public List<double> PuntosX { get; set; } = new();
        public List<double> PuntosY { get; set; } = new();
    }
}
