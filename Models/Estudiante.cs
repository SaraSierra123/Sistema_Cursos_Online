using System;

namespace GestionCursosOnline.Models
{
    public class Estudiante : Usuario
    {
        public string Nivel { get; set; } = string.Empty; // Ej. Principiante, Intermedio, Avanzado

        public override string ToString()
        {
            return base.ToString() + $" | Nivel: {Nivel}";
        }
    }
}
