using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SistemaCursosOnline.Models;
using SistemaCursosOnline.Interfaces;

namespace SistemaCursosOnline.Data
{
    public class EstudianteRepository : ICrud<Estudiante>
    {
        private string rutaDelArchivo = "Data/estudiantes.csv";

        public EstudianteRepository()
        {
            if (Directory.Exists("Data") == false)
            {
                Directory.CreateDirectory("Data");
            }
        }

        public void Crear(Estudiante nuevoEstudiante)
        {
            List<Estudiante> listaDeEstudiantes = Listar();
            listaDeEstudiantes.Add(nuevoEstudiante);
            GuardarTodoEnCsv(listaDeEstudiantes);
        }

        public List<Estudiante> Listar()
        {
            if (File.Exists(rutaDelArchivo) == false)
            {
                return new List<Estudiante>();
            }

            using (StreamReader lectorDeArchivos = new StreamReader(rutaDelArchivo))
            {
                CsvConfiguration configuracionCsv = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (CsvReader lectorCsv = new CsvReader(lectorDeArchivos, configuracionCsv))
                {
                    IEnumerable<Estudiante> registrosLeidos = lectorCsv.GetRecords<Estudiante>();
                    return new List<Estudiante>(registrosLeidos);
                }
            }
        }

        public void Actualizar(Estudiante estudianteEditado)
        {
            List<Estudiante> listaDeEstudiantes = Listar();
            
            for (int indice = 0; indice < listaDeEstudiantes.Count; indice = indice + 1)
            {
                if (listaDeEstudiantes[indice].Id == estudianteEditado.Id)
                {
                    listaDeEstudiantes[indice] = estudianteEditado;
                }
            }

            GuardarTodoEnCsv(listaDeEstudiantes);
        }

        public void Eliminar(int idDelEstudiante)
        {
            List<Estudiante> listaDeEstudiantes = Listar();
            List<Estudiante> listaActualizada = new List<Estudiante>();

            foreach(Estudiante estudianteActual in listaDeEstudiantes)
            {
                if (estudianteActual.Id != idDelEstudiante)
                {
                    listaActualizada.Add(estudianteActual);
                }
            }

            GuardarTodoEnCsv(listaActualizada);
        }

        public Estudiante BuscarPorId(int idDelEstudiante)
        {
            List<Estudiante> listaDeEstudiantes = Listar();
            foreach(Estudiante estudianteActual in listaDeEstudiantes)
            {
                if (estudianteActual.Id == idDelEstudiante)
                {
                    return estudianteActual;
                }
            }
            return null; // Retorna nulo si no lo encuentra
        }

        private void GuardarTodoEnCsv(List<Estudiante> listaParaGuardar)
        {
            using (StreamWriter escritorDeArchivos = new StreamWriter(rutaDelArchivo))
            {
                CsvConfiguration configuracionCsv = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (CsvWriter escritorCsv = new CsvWriter(escritorDeArchivos, configuracionCsv))
                {
                    escritorCsv.WriteRecords(listaParaGuardar);
                }
            }
        }
    }
}