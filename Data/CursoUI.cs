using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using GestionCursosOnline.Models;

namespace GestionCursosOnline.UI
{
    public class CursoUI
    {
        private readonly string archivoCsv = "cursos.csv";

        public void Gestionar()
        {
            int opcion = 0;
            do
            {
                Console.WriteLine("\n--- GESTIÓN DE CURSOS ---");
                Console.WriteLine("1. Crear Curso");
                Console.WriteLine("2. Listar Cursos");
                Console.WriteLine("3. Actualizar Curso");
                Console.WriteLine("4. Eliminar Curso");
                Console.WriteLine("5. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1: Crear(); break;
                        case 2: Listar(); break;
                        case 3: Actualizar(); break;
                        case 4: Eliminar(); break;
                        case 5: Console.WriteLine("Volviendo..."); break;
                        default: Console.WriteLine("Opción no válida."); break;
                    }
                }
            } while (opcion != 5);
        }

        public List<Curso> LeerCursos()
        {
            if (!File.Exists(archivoCsv)) return new List<Curso>();
            try
            {
                using (var reader = new StreamReader(archivoCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<Curso>().ToList();
                }
            }
            catch
            {
                return new List<Curso>();
            }
        }

        private void GuardarCursos(List<Curso> cursos)
        {
            using (var writer = new StreamWriter(archivoCsv))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(cursos);
            }
        }

        public void Crear()
        {
            var cursos = LeerCursos();
            var nuevo = new Curso();
            
            nuevo.Id = cursos.Count > 0 ? cursos.Max(c => c.Id) + 1 : 1;

            Console.Write("Título del Curso: ");
            nuevo.Titulo = Console.ReadLine() ?? "";
            
            Console.Write("Descripción: ");
            nuevo.Descripcion = Console.ReadLine() ?? "";
            
            Console.Write("Nombre del Instructor: ");
            nuevo.Instructor.Nombre = Console.ReadLine() ?? "";

            Console.Write("Especialidad del Instructor: ");
            nuevo.Instructor.Especialidad = Console.ReadLine() ?? "";

            cursos.Add(nuevo);
            GuardarCursos(cursos);
            Console.WriteLine("Curso creado con éxito.");
        }

        public void Listar()
        {
            var cursos = LeerCursos();
            Console.WriteLine("\n--- LISTA DE CURSOS ---");
            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos registrados.");
                return;
            }
            foreach (var cur in cursos)
            {
                Console.WriteLine(cur.ToString());
            }
        }

        public void Actualizar()
        {
            Listar();
            var cursos = LeerCursos();
            if(cursos.Count == 0) return;

            Console.Write("Ingrese el ID del curso a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var curso = cursos.FirstOrDefault(c => c.Id == id);
                if (curso != null)
                {
                    Console.Write($"Nuevo Título ({curso.Titulo}): ");
                    string? tit = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(tit)) curso.Titulo = tit;

                    Console.Write($"Nueva Descripción ({curso.Descripcion}): ");
                    string? desc = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(desc)) curso.Descripcion = desc;

                    GuardarCursos(cursos);
                    Console.WriteLine("Curso actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("Curso no encontrado.");
                }
            }
        }

        public void Eliminar()
        {
            Listar();
            var cursos = LeerCursos();
            if(cursos.Count == 0) return;

            Console.Write("Ingrese el ID del curso a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var curso = cursos.FirstOrDefault(c => c.Id == id);
                if (curso != null)
                {
                    cursos.Remove(curso);
                    GuardarCursos(cursos);
                    Console.WriteLine("Curso eliminado correctamente.");
                }
                else
                {
                    Console.WriteLine("Curso no encontrado.");
                }
            }
        }
    }
}