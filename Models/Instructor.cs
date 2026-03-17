using System;

namespace GestionCursosOnline.Models
{
    public class Instructor : Usuario
    {
        public string Especialidad { get; set; } = string.Empty;

        public override string ToString()
        {
            return base.ToString() + $" | Especialidad: {Especialidad}";
        }
    }
}