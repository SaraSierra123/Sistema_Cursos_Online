using System;

namespace GestionCursosOnline.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"ID: {Id} | Nombre: {Nombre} | Email: {Email}";
        }
    }
}