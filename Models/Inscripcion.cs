using System;

namespace GestionCursosOnline.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        
        // Asociación
        public Estudiante Estudiante { get; set; } = new Estudiante();
        public Curso Curso { get; set; } = new Curso();
        
        public DateTime FechaInscripcion { get; set; }

        public Inscripcion()
        {
            FechaInscripcion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Inscripcion ID: {Id} | Estudiante: {Estudiante.Nombre} | Curso: {Curso.Titulo} | Fecha: {FechaInscripcion.ToShortDateString()}";
        }
    }
}
