using AnlisisNumericoWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AnlisisNumericoWeb.Service
{
    public class EcuacionesService
    {
        public static class SistemasEcuaciones
        {
            public static List<double> GaussJordan(List<List<double>> matriz, List<double> b)
            {
                int n = b.Count;

                // Crear matriz aumentada
                var a = new List<List<double>>();
                for (int i = 0; i < n; i++)
                {
                    var fila = new List<double>(matriz[i]);
                    fila.Add(b[i]); // Agrega el término independiente al final de la fila
                    a.Add(fila);
                }

                // Eliminación Gauss-Jordan
                for (int i = 0; i < n; i++)
                {
                    double pivote = a[i][i];
                    if (Math.Abs(pivote) < 1e-12)
                        throw new Exception($"Pivote cero encontrado en fila {i}.");

                    // Normalizar fila del pivote
                    for (int j = 0; j <= n; j++)
                    {
                        a[i][j] /= pivote;
                    }

                    // Eliminar otros valores en la columna
                    for (int k = 0; k < n; k++)
                    {
                        if (k != i)
                        {
                            double factor = a[k][i];
                            for (int j = 0; j <= n; j++)
                            {
                                a[k][j] -= factor * a[i][j];
                            }
                        }
                    }
                    Debug.WriteLine(i);

                }
                // Extraer resultados (última columna)
                var x = new List<double>();
                for (int i = 0; i < n; i++)
                {
                    x.Add(a[i][n]);
                }

                return x;
            }

            public static List<double> GaussSeidel(List<List<double>> A, List<double> b, double tolerancia, int maxIteraciones)
            {
                int n = b.Count;
                var x = new List<double>(new double[n]);
                var xAnterior = new List<double>(new double[n]);

                for (int iter = 0; iter < maxIteraciones; iter++)
                {

                    Debug.WriteLine(iter);
                    for (int i = 0; i < n; i++)
                    {
                        double suma = b[i];
                        for (int j = 0; j < n; j++)
                        {
                            if (j != i)
                                suma -= A[i][j] * x[j];
                        }
                        x[i] = suma / A[i][i];
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
