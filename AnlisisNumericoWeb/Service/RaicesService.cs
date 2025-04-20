using System;
namespace AnlisisNumericoWeb.Service
{
    public class RaicesService
    {
        public static double CalcularXr(string metodo, Func<double, double> f, double xi, double xd)
        {
            switch (metodo.ToLower())
            {
                case "biseccion":
                    return (xi + xd) / 2;
                case "reglafalsa":
                case "secante":
                    double fxi = f(xi);
                    double fxd = f(xd);
                    return (fxd * xi - fxi * xd) / (fxd - fxi);
                case "tangente":
                    double derivada = (f(xi + 0.0001) - f(xi)) / 0.0001;
                    if (double.IsNaN(derivada) || Math.Abs(derivada) < 1e-12)
                        throw new InvalidOperationException("Derivada muy cercana a cero o no definida");
                    return xi - f(xi) / derivada;
                default:
                    throw new ArgumentException("Método no reconocido");
            }
        }

        public static double BuscarRaiz(Func<double, double> f, string metodo, int iteraciones, double tolerancia, double xi, double? xd = null)
        {
            if (f == null || string.IsNullOrEmpty(metodo) || iteraciones <= 0 || tolerancia <= 0)
                throw new ArgumentException("Parámetros inválidos");

            double fx = f(xi);
            if (Math.Abs(fx) < tolerancia) return xi;

            if ((metodo.ToLower() == "biseccion" || metodo.ToLower() == "reglafalsa") && xd.HasValue)
            {
                double fxi = f(xi);
                double fxd = f(xd.Value);
                if (fxi * fxd > 0)
                    throw new ArgumentException("Los extremos no encierran una raíz");
                if (fxi == 0) return xi;
                if (fxd == 0) return xd.Value;
            }

            double xr = 0, xrAnterior = 0, error = 0;

            for (int i = 1; i <= iteraciones; i++)
            {
                try
                {
                    xr = CalcularXr(metodo, f, xi, xd ?? xi);
                }
                catch
                {
                    Console.WriteLine("El método diverge.");
                    break;
                }

                if (double.IsNaN(xr))
                {
                    Console.WriteLine("El método diverge.");
                    break;
                }

                error = i == 1 ? double.MaxValue : Math.Abs((xr - xrAnterior) / xr);

                if (Math.Abs(f(xr)) < tolerancia || error < tolerancia)
                    return xr;

                if (metodo.ToLower() == "tangente")
                {
                    xi = xr;
                }
                else if (metodo.ToLower() == "biseccion" || metodo.ToLower() == "reglafalsa" || metodo.ToLower() == "secante")
                {
                    if (f(xi) * f(xr) > 0)
                        xi = xr;
                    else
                        xd = xr;
                }

                xrAnterior = xr;
            }

            return xr;
        }
    }

}

