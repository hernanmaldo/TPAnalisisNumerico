using System.ComponentModel.DataAnnotations;

namespace AnlisisNumericoWeb.Models
{
    public class RaizData
    {
        [Required(ErrorMessage = "La función es obligatoria.")]
        public string Funcion { get; set; }

        [Required(ErrorMessage = "El límite inferior es obligatorio.")]
        public double Xi { get; set; }

        [Required(ErrorMessage = "El límite superior es obligatorio.")]
        public double Xd { get; set; }

        [Required(ErrorMessage = "La tolerancia es obligatoria.")]
        [Range(0.00001, 1, ErrorMessage = "La tolerancia debe estar entre 0.00001 y 1.")]
        public double Tolerancia { get; set; }

        [Required(ErrorMessage = "Debes indicar el número de iteraciones.")]
        [Range(1, 1000, ErrorMessage = "Debe ser entre 1 y 1000.")]
        public int Iteraciones { get; set; }
        public double? Raiz { get; set; } = null;

    }
}
