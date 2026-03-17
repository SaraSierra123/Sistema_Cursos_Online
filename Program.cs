using System;
using GestionCursosOnline.UI;

namespace GestionCursosOnline
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;
            var estudianteUI = new EstudianteUI();
            var cursoUI = new CursoUI();
            var inscripcionUI = new InscripcionUI();

            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("  SISTEMA DE GESTIÓN DE CURSOS ONLINE");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Gestionar estudiantes");
                Console.WriteLine("2. Gestionar cursos");
                Console.WriteLine("3. Gestionar inscripciones");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            estudianteUI.Gestionar();
                            break;
                        case 2:
                            cursoUI.Gestionar();
                            break;
                        case 3:
                            inscripcionUI.Gestionar();
                            break;
                        case 4:
                            Console.WriteLine("Saliendo del sistema...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida, intente de nuevo.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Por favor ingrese un número.");
                }
            } while (opcion != 4);
        }
    }
}