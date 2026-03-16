using System.Collections.Generic;

namespace SistemaCursosOnline.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CupoMaximo { get; set; }
        
        // Relacion de Agregacion: Un curso guarda la referencia al ID del Instructor.
        // Si el curso se borra, el instructor sigue vivo en el sistema.
        public int InstructorId { get; set; }
        
        // Relacion de Composicion: Un curso está fuertemente compuesto de modulos.
        public List<Modulo> ListaDeModulos { get; set; }

        public Curso()
        {
            ListaDeModulos = new List<Modulo>();
        }

        public void AgregarModulo(Modulo nuevoModulo)
        {
            ListaDeModulos.Add(nuevoModulo);
        }
    }
}
