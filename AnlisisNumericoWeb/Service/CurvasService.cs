using AnlisisNumericoWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AnlisisNumericoWeb.Service
{
    public class CurvasService
    {
        public CurvaResult Calcular(List<Punto> puntos, string tipo, int grado = 3)
        {


            if (tipo == "Polinomial")
            {

                if (puntos.Count < 3)
                {
                    throw new ArgumentException("Se requieren como minimo 3 puntos");
                }

                return CalcularPolinomial(puntos, grado);

            }
            else
            {
                if (puntos.Count < 2)
                {
                    throw new ArgumentException("Se requieren como minimo 2 puntos");
                }
                return CalcularLineal(puntos);
            }

        }

        private CurvaResult CalcularPolinomial(List<Punto> puntosCargados, int grado)
        {

            List<double[]> PuntosCargados = new List<double[]>();

            foreach (Punto i in puntosCargados)
            {
                PuntosCargados.Add([i.X, i.Y]);
            }

            var matriz = GenerarMatrizPolinomial(grado, PuntosCargados);
            var coeficientes = ResolverSistemaGaussJordan(matriz);
            string funcion = GenerarFuncionPolinomial(coeficientes);
            double r = CalcularCoeficienteR(PuntosCargados, coeficientes);

            return new CurvaResult
            {
                Coeficientes = coeficientes,
                Funcion = funcion,
                Correlacion = r
            };

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

            double St = 0;
            double Sr = 0;
            foreach (var punto in puntos)
            {
                St += Math.Pow(((sumY / n) - punto.Y), 2);
                Sr += Math.Pow((pendiente * punto.X + ordenada - punto.Y), 2);
            }

            double r = Math.Sqrt((St - Sr) / St) * 100;

            return new CurvaResult
            {
                Coeficientes = new double[] { pendiente, ordenada },
                Funcion = $"y = {pendiente:F2}x + {ordenada:F2}",
                Tipo = "Lineal",
                Correlacion = Math.Round(r, 2)
            };
        }


        public double[,] GenerarMatrizPolinomial(int grado, List<double[]> puntosCargados)
        {
            int dimension = grado + 1;
            double[,] matriz = new double[dimension, dimension + 1];

            foreach (var punto in puntosCargados)
            {
                double x = punto[0];
                double y = punto[1];

                for (int fila = 0; fila < dimension; fila++)
                {
                    for (int col = 0; col < dimension; col++)
                    {
                        matriz[fila, col] += Math.Pow(x, fila + col);
                    }
                    matriz[fila, dimension] += y * Math.Pow(x, fila);
                }
            }

            return matriz;
        }

        public double[] ResolverSistemaGaussJordan(double[,] matriz)
        {
            int n = matriz.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                // Normaliza la fila
                double divisor = matriz[i, i];
                for (int j = 0; j <= n; j++)
                    matriz[i, j] /= divisor;

                // Elimina las otras filas
                for (int k = 0; k < n; k++)
                {
                    if (k == i) continue;
                    double factor = matriz[k, i];
                    for (int j = 0; j <= n; j++)
                        matriz[k, j] -= factor * matriz[i, j];
                }
            }

            double[] resultado = new double[n];
            for (int i = 0; i < n; i++)
                resultado[i] = matriz[i, n];

            return resultado;
        }

        public string GenerarFuncionPolinomial(double[] coeficientes)
        {
            string funcion = string.Empty;

            for (int i = coeficientes.Length - 1; i >= 0; i--)
            {
                double ai = Math.Round(coeficientes[i], 4);
                if (ai == 0) continue;

                string signo = ai > 0 && funcion.Length > 0 ? " + " : ai < 0 ? " - " : "";
                ai = Math.Abs(ai);

                if (i == 0)
                    funcion += $"{signo}{ai}";
                else if (i == 1)
                    funcion += $"{signo}{ai}x";
                else
                    funcion += $"{signo}{ai}x^{i}";
            }

            return funcion;
        }

        public double CalcularCoeficienteR(List<double[]> puntos, double[] coeficientes)
        {
            double sr = 0, st = 0;
            double sumY = puntos.Sum(p => p[1]);
            double promY = sumY / puntos.Count;

            foreach (var punto in puntos)
            {
                double x = punto[0];
                double y = punto[1];

                double estimado = 0;
                for (int i = 0; i < coeficientes.Length; i++)
                {
                    estimado += coeficientes[i] * Math.Pow(x, i);
                }

                sr += Math.Pow(estimado - y, 2);
                st += Math.Pow(promY - y, 2);
            }

            double r2 = (st - sr) / st;
            return Math.Sqrt(Math.Abs(r2)) * 100;  // En caso de errores numéricos
        }
    }
}

    //private CurvaResult CalcularPolinomial(List<Punto> puntos)
    //{
    //    int n = puntos.Count;

    //    double sumX = 0, sumX2 = 0, sumX3 = 0, sumX4 = 0;
    //    double sumY = 0, sumXY = 0, sumX2Y = 0;

    //    foreach (var p in puntos)
    //    {
    //        double x = p.X, y = p.Y;
    //        double x2 = x * x;
    //        double x3 = x2 * x;
    //        double x4 = x3 * x;

    //        sumX += x;
    //        sumX2 += x2;
    //        sumX3 += x3;
    //        sumX4 += x4;
    //        sumY += y;
    //        sumXY += x * y;
    //        sumX2Y += x2 * y;
    //    }

    //    double[,] A = {
    //        { n, sumX, sumX2 },
    //        { sumX, sumX2, sumX3 },
    //        { sumX2, sumX3, sumX4 }
    //    };

    //    double[] B = { sumY, sumXY, sumX2Y };

    //    double[] coef = ResolverSistema(A, B,4);

    //    double St = 0;
    //    double Sr = 0;
    //    foreach (var punto in puntos)
    //    {
    //        double suma = 0;
    //        for(int i = 0; coef.Count() > i; i++)
    //        {
    //            suma += coef[i] * Math.Pow(punto.X , i);

    //        }

    //        St += Math.Pow(((sumY / n) - punto.Y), 2);
    //        Sr += Math.Pow((suma- punto.Y), 2);
    //    }


    //    double r = Math.Sqrt((St - Sr) / St) * 100;

    //    return new CurvaResult
    //    {
    //        Coeficientes = coef,
    //        Funcion = $"y = {coef[2]:F2}x² + {coef[1]:F2}x + {coef[0]:F2}",
    //        Tipo = "Polinomial",
    //        Correlacion = Math.Round(r,2),
    //    };
    //}

    //private double[] ResolverSistema(double[,] A, double[] B, int grado)
    //{

    //    int n = grado;
    //    for (int i = 0; i < n; i++)
    //    {
    //        double diag = A[i, i];
    //        for (int j = 0; j < n; j++) A[i, j] /= diag;
    //        B[i] /= diag;

    //        for (int k = 0; k < n; k++)
    //        {
    //            if (k == i) continue;
    //            double factor = A[k, i];
    //            for (int j = 0; j < n; j++) A[k, j] -= factor * A[i, j];
    //            B[k] -= factor * B[i];
    //        }
    //    }
    //    return B;
    //}

