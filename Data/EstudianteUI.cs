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
    public class EstudianteUI
    {
        private readonly string archivoCsv = "estudiantes.csv";

        public void Gestionar()
        {
            int opcion = 0;
            do
            {
                Console.WriteLine("\n--- GESTIÓN DE ESTUDIANTES ---");
                Console.WriteLine("1. Crear Estudiante");
                Console.WriteLine("2. Listar Estudiantes");
                Console.WriteLine("3. Actualizar Estudiante");
                Console.WriteLine("4. Eliminar Estudiante");
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

        private List<Estudiante> LeerEstudiantes()
        {
            if (!File.Exists(archivoCsv)) return new List<Estudiante>();
            try
            {
                using (var reader = new StreamReader(archivoCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<Estudiante>().ToList();
                }
            }
            catch
            {
                return new List<Estudiante>();
            }
        }

        private void GuardarEstudiantes(List<Estudiante> estudiantes)
        {
            using (var writer = new StreamWriter(archivoCsv))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(estudiantes);
            }
        }

        public void Crear()
        {
            var estudiantes = LeerEstudiantes();
            var nuevo = new Estudiante();
            
            // Generar ID
            nuevo.Id = estudiantes.Count > 0 ? estudiantes.Max(e => e.Id) + 1 : 1;

            Console.Write("Nombre: ");
            nuevo.Nombre = Console.ReadLine() ?? "";
            
            Console.Write("Email: ");
            nuevo.Email = Console.ReadLine() ?? "";
            
            Console.Write("Nivel (Principiante/Intermedio/Avanzado): ");
            nuevo.Nivel = Console.ReadLine() ?? "";

            estudiantes.Add(nuevo);
            GuardarEstudiantes(estudiantes);
            Console.WriteLine("Estudiante creado con éxito.");
        }

        public void Listar()
        {
            var estudiantes = LeerEstudiantes();
            Console.WriteLine("\n--- LISTA DE ESTUDIANTES ---");
            if (estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes registrados.");
                return;
            }
            foreach (var est in estudiantes)
            {
                Console.WriteLine(est.ToString());
            }
        }

        public void Actualizar()
        {
            Listar();
            var estudiantes = LeerEstudiantes();
            if(estudiantes.Count == 0) return;

            Console.Write("Ingrese el ID del estudiante a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);
                if (estudiante != null)
                {
                    Console.Write($"Nuevo Nombre ({estudiante.Nombre}): ");
                    string? nom = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nom)) estudiante.Nombre = nom;

                    Console.Write($"Nuevo Email ({estudiante.Email}): ");
                    string? em = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(em)) estudiante.Email = em;

                    Console.Write($"Nuevo Nivel ({estudiante.Nivel}): ");
                    string? niv = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(niv)) estudiante.Nivel = niv;

                    GuardarEstudiantes(estudiantes);
                    Console.WriteLine("Estudiante actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("Estudiante no encontrado.");
                }
            }
        }

        public void Eliminar()
        {
            Listar();
            var estudiantes = LeerEstudiantes();
            if(estudiantes.Count == 0) return;

            Console.Write("Ingrese el ID del estudiante a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);
                if (estudiante != null)
                {
                    estudiantes.Remove(estudiante);
                    GuardarEstudiantes(estudiantes);
                    Console.WriteLine("Estudiante eliminado correctamente.");
                }
                else
                {
                    Console.WriteLine("Estudiante no encontrado.");
                }
            }
        }
    }
}