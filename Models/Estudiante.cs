namespace SistemaCursosOnline.Models
{
    // Relacion de Herencia: Estudiante hereda de Usuario
    public class Estudiante : Usuario
    {
        public string CodigoEstudiante { get; set; }
    }
}
