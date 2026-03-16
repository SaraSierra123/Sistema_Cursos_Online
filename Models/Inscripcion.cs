namespace SistemaCursosOnline.Models
{
    // Relacion de Asociacion: Conecta la existencia de un Estudiante con un Curso especifico
    public class Inscripcion
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public string FechaInscripcion { get; set; }
    }
}
