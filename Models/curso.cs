using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using GestionCursosOnline.Interfaces;

namespace GestionCursosOnline.Models
{
    public class Curso : IInscribible
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        
        // Relación: el curso tiene un Instructor
        public Instructor Instructor { get; set; } = new Instructor();

        // Agregación: un curso tiene una lista de estudiantes
        [Ignore]
        public List<Estudiante> Estudiantes { get; set; }

        public Curso()
        {
            Estudiantes = new List<Estudiante>();
        }

        public void InscribirEstudiante(Estudiante estudiante)
        {
            Estudiantes.Add(estudiante);
            Console.WriteLine($"Estudiante {estudiante.Nombre} inscrito en el curso {Titulo}.");
        }

        public void MostrarEstudiantes()
        {
            Console.WriteLine($"--- Estudiantes en {Titulo} ---");
            if (Estudiantes.Count == 0)
            {
                Console.WriteLine("No hay estudiantes inscritos aún.");
            }
            else
            {
                foreach (var est in Estudiantes)
                {
                    Console.WriteLine(est.ToString());
                }
            }
            Console.WriteLine("--------------------------------");
        }

        public override string ToString()
        {
            return $"Curso ID: {Id} | Título: {Titulo} | Instructor: {Instructor.Nombre}";
        }
    }
}
