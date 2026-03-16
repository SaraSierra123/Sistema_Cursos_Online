using System.Collections.Generic;

namespace SistemaCursosOnline.Interfaces
{
    public interface ICrud<T>
    {
        void Crear(T nuevoElemento);
        List<T> Listar();
        void Actualizar(T elementoEditado);
        void Eliminar(int idDelElemento);
        T BuscarPorId(int idDelElemento);
    }
}