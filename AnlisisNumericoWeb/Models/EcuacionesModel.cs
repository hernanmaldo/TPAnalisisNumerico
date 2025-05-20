using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AnlisisNumericoWeb.Models
{
    public class EcuacionesModel
    {

        public List<List<double>> Matriz { get; set; } = new();       // Coeficientes a
        public List<double> VectorIndependiente { get; set; } = new();  // Términos independientes  b
        public string Metodo { get; set; }            // "gauss-jordan" o "gauss-seidel"
        public double Tolerancia { get; set; } = 0.0001;
        public int Iteraciones { get; set; } = 100;

        public List<double> Incognitas { get; set; } =  new();

        public List<List<string>> MatrizTexto
        {
            get => Matriz.Select(row => row
                    .Select(d => d.ToString("0.###", CultureInfo.InvariantCulture))
                    .ToList()).ToList();

            set => Matriz = value.Select(row => row
                    .Select(s =>
                    {
                        var str = s.Replace(',', '.');
                        return double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                            ? result
                            : 0;
                    }).ToList()).ToList();
        }

        public List<string> VectorIndependienteTexto
        {
            get => VectorIndependiente
                .Select(d => d.ToString("0.###", CultureInfo.InvariantCulture))
                .ToList();

            set => VectorIndependiente = value.Select(s =>
            {
                var str = s.Replace(',', '.');
                return double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                    ? result
                    : 0;
            }).ToList();
        }
    }
}

