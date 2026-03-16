namespace SistemaCursosOnline.Models
{
    // Relacion de Herencia: Instructor hereda de Usuario
    public class Instructor : Usuario
    {
        public string Especialidad { get; set; }
    }
}
