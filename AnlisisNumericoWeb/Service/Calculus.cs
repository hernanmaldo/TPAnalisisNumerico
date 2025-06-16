//using Calculus;


//namespace AnlisisNumericoWeb.Service
//{
//    /*
//    public class Calculus
//    {



//    // Instanciarla 
//Calculo analizadorFuncion = new Calculo();

//public string Prueba()
//{
//   string funcion = "2-2";
//   double xi = 1;
//   double xd = 5;
//   double tolerancia = 0.0001;
//   int iteraciones = 186;

//   // Para evaluar la sintaxis correcta 
//   if (analizadorFuncion.Sintaxis(funcion, 'x'))
//   {
//       // Para evaluar la función en el punto 
//       if (analizadorFuncion.EvaluaFx(xi) * analizadorFuncion.EvaluaFx(xd) > 0)
//       {
//           return string.Format("El intervalo [{0}, {1}] no contiene a la raíz. Vuelva a ingresar xi y xd.", xi, xd);
//       }
//       else
//       {
//           // Para calcular la derivada para el método de la Tangente 
//           var derivada = analizadorFuncion.Dx(xi);

//           // Para controlar si la derivada no es NAN (Not a Number) 
//           if (derivada < tolerancia || double.IsNaN(derivada))
//           {
//               return "La derivada es menor a la tolerancia o no es un número. El método diverge ya que no encuentra la raíz. Raíz no encontrada";
//           }
//           else
//           {
//               // Para hacer el bucle 
//               for (int i = 1; i <= iteraciones; i++)
//               {
//                   // Para hacer el absoluto 
//                   if (Math.Abs(analizadorFuncion.EvaluaFx(xi)) < tolerancia)
//                   {
//                       // Para cortar el bucle 
//                       return string.Format("El valor del extremo izquierdo ingresado es la raíz. La raíz es igual a {0}", xi);

//                   }
//               }
//           }
//       }
//   }
//   else
//   {
//       return "La sintaxis de la función no es correcta. Vuelva a ingresarla, Sintaxis incorrecta";
//   }
//       return "Ni idea";
//}
//}


//}
// */




