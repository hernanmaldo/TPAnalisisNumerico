using AnlisisNumericoWeb.Models;
using System;

namespace AnlisisNumericoWeb.Service
{
    public class EcuacionesService
    {
        public static class SistemasEcuaciones
        {
            //Este método resuelve un sistema de ecuaciones lineales 𝐴𝑥=𝑏
            //Ax=b usando el método de eliminación de Gauss-Jordan, y devuelve el vector solución x.
            public static double[] GaussJordan(double[,] matriz, double[] b)
            {

                //n: número de ecuaciones.
                //a: matriz aumentada con n filas y n + 1 columnas(la última columna es b).

                int n = b.Length;
                double[,] a = new double[n, n + 1];

                //Copia los coeficientes de matriz y b a la matriz aumentada a.

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        a[i, j] = matriz[i, j];
                    }
                    a[i, n] = b[i];
                }

                for (int i = 0; i < n; i++)
                {
                    double pivote = a[i, i];
                    if (pivote == 0)
                        throw new Exception("Pivote cero encontrado.");

                    for (int j = 0; j <= n; j++)
                    {
                        a[i, j] /= pivote;
                    }

                    for (int k = 0; k < n; k++)
                    {
                        if (k != i)
                        {
                            double factor = a[k, i];
                            for (int j = 0; j <= n; j++)
                            {
                                a[k, j] -= factor * a[i, j];
                            }
                        }
                    }
                }

                double[] x = new double[n];
                for (int i = 0; i < n; i++)
                {
                    x[i] = a[i, n];
                }

                return x;
            }

            public static double[] GaussSeidel(double[,] A, double[] b, double tolerancia, int maxIteraciones)
            {
                int n = b.Length;
                double[] x = new double[n];
                double[] xAnterior = new double[n];

                for (int iter = 0; iter < maxIteraciones; iter++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        double suma = b[i];
                        for (int j = 0; j < n; j++)
                        {
                            if (j != i)
                                suma -= A[i, j] * x[j];
                        }
                        x[i] = suma / A[i, i];
                    }

                    double error = 0.0;
                    for (int i = 0; i < n; i++)
                    {
                        error += Math.Abs(x[i] - xAnterior[i]);
                        xAnterior[i] = x[i];
                    }

                    if (error < tolerancia)
                        break;
                }

                return x;
            }
        }
    }
}
