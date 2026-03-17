using System.Collections.Generic;

namespace GestionCursosOnline.Interfaces
{
    using GestionCursosOnline.Models;
    
    public interface IInscribible
    {
        void InscribirEstudiante(Estudiante estudiante);
        void MostrarEstudiantes();
    }
}