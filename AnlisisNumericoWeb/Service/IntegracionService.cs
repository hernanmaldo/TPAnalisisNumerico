using AnlisisNumericoWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using NCalc;


namespace AnlisisNumericoWeb.Service
{
    public class IntegracionService
    {
        private double EvaluarFuncion(string funcion, double x)
        {
            var expr = new NCalc.Expression(funcion);
            expr.Parameters["x"] = x;
            return Convert.ToDouble(expr.Evaluate());
        }
        public double Calcular(IntegracionModel model)
        {
            double a = model.LimiteInferior;
            double b = model.LimiteSuperior;
            int n = model.NumeroIntervalos;

            switch (model.Metodo)
            {
                case "Trapecio Simple":
                    return TrapecioSimple(model.Funcion, a, b);
                case "Trapecio Múltiple":
                    return TrapecioMultiple(model.Funcion, a, b, n);
                case "Simpson 1/3 Simple":
                    return Simpson13Simple(model.Funcion, a, b);
                case "Simpson 1/3 Múltiple":
                    return Simpson13Multiple(model.Funcion, a, b, n);
                case "Simpson 3/8":
                    return Simpson38(model.Funcion, a, b);
                default:
                    throw new ArgumentException("Método no reconocido.");
            }
        }

        private double TrapecioSimple(string funcion, double a, double b)
        {
            return (b - a) * (EvaluarFuncion(funcion, a) + EvaluarFuncion(funcion, b)) / 2.0;
        }

        private double TrapecioMultiple(string funcion, double a, double b, int n)
        {
            double h = (b - a) / n;
            double suma = EvaluarFuncion(funcion, a) + EvaluarFuncion(funcion, b);

            for (int i = 1; i < n; i++)
            {
                double xi = a + i * h;
                suma += 2 * EvaluarFuncion(funcion, xi);
            }

            return (h / 2.0) * suma;
        }

        private double Simpson13Simple(string funcion, double a, double b)
        {
            double h = (b - a) / 2.0;
            double x0 = a;
            double x1 = a + h;
            double x2 = b;

            return (h / 3.0) * (EvaluarFuncion(funcion, x0) + 4 * EvaluarFuncion(funcion, x1) + EvaluarFuncion(funcion, x2));
        }

        private double Simpson13Multiple(string funcion, double a, double b, int n)
        {
            if (n % 2 != 0)
                throw new ArgumentException("n debe ser par para Simpson 1/3 múltiple.");

            double h = (b - a) / n;
            double suma = EvaluarFuncion(funcion, a) + EvaluarFuncion(funcion, b);

            for (int i = 1; i < n; i++)
            {
                double xi = a + i * h;
                suma += (i % 2 == 0 ? 2 : 4) * EvaluarFuncion(funcion, xi);
            }

            return (h / 3.0) * suma;
        }

        private double Simpson38(string funcion, double a, double b)
        {
            double h = (b - a) / 3.0;
            double x0 = a;
            double x1 = a + h;
            double x2 = a + 2 * h;
            double x3 = b;

            return (3 * h / 8.0) * (
                EvaluarFuncion(funcion, x0) +
                3 * EvaluarFuncion(funcion, x1) +
                3 * EvaluarFuncion(funcion, x2) +
                EvaluarFuncion(funcion, x3)
            );
        }
    }
}
