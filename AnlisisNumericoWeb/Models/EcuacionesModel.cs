using System.ComponentModel.DataAnnotations;

namespace AnlisisNumericoWeb.Models
{
    public class EcuacionesModel
    {
        public double[,] Matriz { get; set; }         // Coeficientes (A)
        public double[] VectorIndependiente { get; set; }  // Términos independientes (b)
        public string Metodo { get; set; }            // "gauss-jordan" o "gauss-seidel"
        public double Tolerancia { get; set; } = 0.0001;
        public int Iteraciones { get; set; } = 100;
    }
}
