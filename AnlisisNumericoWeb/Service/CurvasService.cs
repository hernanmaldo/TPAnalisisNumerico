using AnlisisNumericoWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AnlisisNumericoWeb.Service
{
    public class CurvasService
    {
        public CurvaResult Calcular(List<Punto> puntos, string tipo)
        {
            if (tipo == "Polinomial")
                return CalcularPolinomial(puntos);
            else
                return CalcularLineal(puntos);
        }

        private CurvaResult CalcularLineal(List<Punto> puntos)
        {
            int n = puntos.Count;
            double sumX = puntos.Sum(p => p.X);
            double sumY = puntos.Sum(p => p.Y);
            double sumXY = puntos.Sum(p => p.X * p.Y);
            double sumX2 = puntos.Sum(p => p.X * p.X);

            double pendiente = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX);
            double ordenada = (sumY - pendiente * sumX) / n;

            return new CurvaResult
            {
                Coeficientes = new double[] { pendiente, ordenada },
                Funcion = $"y = {pendiente:F2}x + {ordenada:F2}",
                Tipo = "Lineal"
            };
        }

        private CurvaResult CalcularPolinomial(List<Punto> puntos)
        {
            int n = puntos.Count;

            double sumX = 0, sumX2 = 0, sumX3 = 0, sumX4 = 0;
            double sumY = 0, sumXY = 0, sumX2Y = 0;

            foreach (var p in puntos)
            {
                double x = p.X, y = p.Y;
                double x2 = x * x;
                double x3 = x2 * x;
                double x4 = x3 * x;

                sumX += x;
                sumX2 += x2;
                sumX3 += x3;
                sumX4 += x4;
                sumY += y;
                sumXY += x * y;
                sumX2Y += x2 * y;
            }

            double[,] A = {
                { n, sumX, sumX2 },
                { sumX, sumX2, sumX3 },
                { sumX2, sumX3, sumX4 }
            };

            double[] B = { sumY, sumXY, sumX2Y };

            double[] coef = ResolverSistema3x3(A, B);

            return new CurvaResult
            {
                Coeficientes = coef,
                Funcion = $"y = {coef[2]:F2}x² + {coef[1]:F2}x + {coef[0]:F2}",
                Tipo = "Polinomial"
            };
        }

        private double[] ResolverSistema3x3(double[,] A, double[] B)
        {
            // Gauss-Jordan simple
            int n = 3;
            for (int i = 0; i < n; i++)
            {
                double diag = A[i, i];
                for (int j = 0; j < n; j++) A[i, j] /= diag;
                B[i] /= diag;

                for (int k = 0; k < n; k++)
                {
                    if (k == i) continue;
                    double factor = A[k, i];
                    for (int j = 0; j < n; j++) A[k, j] -= factor * A[i, j];
                    B[k] -= factor * B[i];
                }
            }
            return B;
        }
    }
}
