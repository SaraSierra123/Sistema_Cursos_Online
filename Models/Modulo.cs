namespace SistemaCursosOnline.Models
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int DuracionHoras { get; set; }
        public int CursoId { get; set; }
    }
}
