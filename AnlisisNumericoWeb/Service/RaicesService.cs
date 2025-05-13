using AnlisisNumericoWeb.Models;
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

                    double control = Math.Abs(fxd - fxi);
                    if (Math.Abs(fxd - fxi) < 0.0001)
                        throw new InvalidOperationException("La diferencia entre f(xd) y f(xi) es demasiado pequeña");
                    return (fxd * xi - fxi * xd) / (fxd - fxi);
                case "tangente":
                    double h = 0.0001;
                    double derivada = (f(xi + h) - f(xi - h)) / (2 * h);
                    if (double.IsNaN(derivada) || Math.Abs(derivada) < 0.0001)
                        throw new InvalidOperationException("Derivada muy cercana a cero o no definida");
                    return xi - f(xi) / derivada;
                default:
                    throw new ArgumentException("Método no reconocido");
            }
        }

        public static double BuscarRaiz(Func<double, double> f, string metodo, RaizData data)
        {
           
            if (f == null || string.IsNullOrEmpty(metodo) || data.Iteraciones <= 0 || data.Tolerancia <= 0)
                throw new ArgumentException("Parámetros inválidos");

            double xi = data.Xi;
            double xd = data.Xd;
            double tolerancia = data.Tolerancia;
            int iteraciones = data.Iteraciones;
            data.Iteraciones = 0;

            double fx = f(xi);
            //if (Math.Abs(fx) < tolerancia) return xi;
    
              

            if ((metodo.ToLower() == "biseccion" || metodo.ToLower() == "reglafalsa") && xd != xi)
            {
                double fxi = f(xi);
                double fxd = f(xd);
                if (fxi * fxd > 0)
                    throw new ArgumentException("Los extremos no encierran una raíz");
                if (fxi == 0) return xi;
                if (fxd == 0) return xd;
            }

            double xr = 0, xrAnterior = 0, error = 0;
            int count = 0;

            
            for (int i = 1; i <= iteraciones; i++)
            {
                data.Iteraciones = i;
                
                
                xr = CalcularXr(metodo, f, xi, xd);
                
             

                if (double.IsNaN(xr))
                {
                    throw new ArgumentException("El método diverge.");
                    
                }

                error = i == 1 ? double.MaxValue : Math.Abs((xr - xrAnterior) / xr);

                if (Math.Abs(f(xr)) < tolerancia || error < tolerancia)
                {
                    data.Raiz = xr;
                    return xr;
                }

                if (metodo.ToLower() == "tangente")
                {
                    xi = xr;
                }
                else if (metodo.ToLower() == "biseccion" || metodo.ToLower() == "reglafalsa")
                {
                    if (f(xi) * f(xr) > 0)
                        xi = xr;
                    else
                        xd = xr;
                }
                else if (metodo.ToLower() == "secante")
                {
                    double fxi = f(xi);
                    double fxr = f(xr);

                   
                    if (Math.Abs(fxr - fxi) < 1e-12)
                        throw new InvalidOperationException("Diferencia de f(x) muy pequeña");

                
                    xd = xi;
                    xi = xr;
                }

                xrAnterior = xr;
                count = i;
            }


            
            data.Raiz = xr;
            return xr;
        }

    }

}

