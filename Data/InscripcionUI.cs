using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using GestionCursosOnline.Models;

namespace GestionCursosOnline.UI
{
    public class InscripcionUI
    {
        private readonly string archivoCsv = "inscripciones.csv";
        private readonly EstudianteUI estudianteUI = new EstudianteUI();
        private readonly CursoUI cursoUI = new CursoUI();

        public void Gestionar()
        {
            int opcion = 0;
            do
            {
                Console.WriteLine("\n--- GESTIÓN DE INSCRIPCIONES ---");
                Console.WriteLine("1. Crear Inscripción");
                Console.WriteLine("2. Listar Inscripciones");
                Console.WriteLine("3. Eliminar Inscripción");
                Console.WriteLine("4. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1: Crear(); break;
                        case 2: Listar(); break;
                        case 3: Eliminar(); break;
                        case 4: Console.WriteLine("Volviendo..."); break;
                        default: Console.WriteLine("Opción no válida."); break;
                    }
                }
            } while (opcion != 4);
        }

        private List<Inscripcion> LeerInscripciones()
        {
            if (!File.Exists(archivoCsv)) return new List<Inscripcion>();
            try
            {
                using (var reader = new StreamReader(archivoCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<Inscripcion>().ToList();
                }
            }
            catch
            {
                return new List<Inscripcion>();
            }
        }

        private void GuardarInscripciones(List<Inscripcion> inscripciones)
        {
            using (var writer = new StreamWriter(archivoCsv))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(inscripciones);
            }
        }

        public void Crear()
        {
            var inscripciones = LeerInscripciones();
            var nueva = new Inscripcion();
            
            nueva.Id = inscripciones.Count > 0 ? inscripciones.Max(i => i.Id) + 1 : 1;
            nueva.FechaInscripcion = DateTime.Now;

            Console.WriteLine("--- Seleccione un Estudiante ---");
            estudianteUI.Listar();
            Console.Write("ID del Estudiante: ");
            if (!int.TryParse(Console.ReadLine(), out int estId)) return;

            Console.WriteLine("--- Seleccione un Curso ---");
            cursoUI.Listar();
            Console.Write("ID del Curso: ");
            if (!int.TryParse(Console.ReadLine(), out int cursoId)) return;

            // Para simplicidad, se pueden rellenar los datos completos leyendo los CSVs reales
            // pero para esta tarea de consola y archivo plano con CsvHelper,
            // simplemente asignamos los IDs y algunos nombres básicos como muestra.
            nueva.Estudiante.Id = estId;
            nueva.Curso.Id = cursoId;
            
            // Rehidratar objeto buscando en las listas (Opcional, pero da más información)
            var estudiante = LeerEstudiantes().FirstOrDefault(e => e.Id == estId);
            var curso = cursoUI.LeerCursos().FirstOrDefault(c => c.Id == cursoId);

            if (estudiante == null || curso == null)
            {
                Console.WriteLine("Error: Estudiante o Curso no encontrado.");
                return;
            }

            nueva.Estudiante = estudiante;
            nueva.Curso = curso;

            inscripciones.Add(nueva);
            GuardarInscripciones(inscripciones);
            
            // Aplicar la interfaz IInscribible (Polimorfismo/Interfaces)
            curso.InscribirEstudiante(estudiante);

            Console.WriteLine("Inscripción creada con éxito.");
        }

        private List<Estudiante> LeerEstudiantes()
        {
            if (!File.Exists("estudiantes.csv")) return new List<Estudiante>();
            using (var reader = new StreamReader("estudiantes.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Estudiante>().ToList();
            }
        }

        public void Listar()
        {
            var inscripciones = LeerInscripciones();
            Console.WriteLine("\n--- LISTA DE INSCRIPCIONES ---");
            if (inscripciones.Count == 0)
            {
                Console.WriteLine("No hay inscripciones registradas.");
                return;
            }
            foreach (var ins in inscripciones)
            {
                Console.WriteLine(ins.ToString());
            }
        }

        public void Eliminar()
        {
            Listar();
            var inscripciones = LeerInscripciones();
            if(inscripciones.Count == 0) return;

            Console.Write("Ingrese el ID de la inscripción a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var inscripcion = inscripciones.FirstOrDefault(i => i.Id == id);
                if (inscripcion != null)
                {
                    inscripciones.Remove(inscripcion);
                    GuardarInscripciones(inscripciones);
                    Console.WriteLine("Inscripción eliminada correctamente.");
                }
                else
                {
                    Console.WriteLine("Inscripción no encontrada.");
                }
            }
        }
    }
}