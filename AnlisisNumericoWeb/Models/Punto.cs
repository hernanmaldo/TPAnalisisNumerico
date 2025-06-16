using System.ComponentModel.DataAnnotations;

namespace AnlisisNumericoWeb.Models
{
    public class Punto
    {
        public Punto()
        {
            X = 0; Y = 0;

        }
        public double X { get; set; }
        public double Y { get; set; }

        public string x 
        {
            get => X.ToString();
            set
            {
                this.X = double.Parse(value);   
            }
        }   
        public string y 
        {
            get => Y.ToString();
            set
            {
                this.Y = double.Parse(value);
            }
        }
    }
}
