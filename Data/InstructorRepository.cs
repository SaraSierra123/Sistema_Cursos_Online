using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SistemaCursosOnline.Models;
using SistemaCursosOnline.Interfaces;

namespace SistemaCursosOnline.Data
{
    public class InstructorRepository : ICrud<Instructor>
    {
        private string rutaDelArchivo = "Data/instructores.csv";

        public InstructorRepository()
        {
            if (Directory.Exists("Data") == false)
            {
                Directory.CreateDirectory("Data");
            }
        }

        public void Crear(Instructor nuevoInstructor)
        {
            List<Instructor> listaDeInstructores = Listar();
            listaDeInstructores.Add(nuevoInstructor);
            GuardarTodoEnCsv(listaDeInstructores);
        }

        public List<Instructor> Listar()
        {
            if (File.Exists(rutaDelArchivo) == false)
            {
                return new List<Instructor>();
            }

            using (StreamReader lectorDeArchivos = new StreamReader(rutaDelArchivo))
            {
                CsvConfiguration configuracionCsv = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (CsvReader lectorCsv = new CsvReader(lectorDeArchivos, configuracionCsv))
                {
                    IEnumerable<Instructor> registrosLeidos = lectorCsv.GetRecords<Instructor>();
                    return new List<Instructor>(registrosLeidos);
                }
            }
        }

        public void Actualizar(Instructor instructorEditado)
        {
            List<Instructor> listaDeInstructores = Listar();
            
            for (int indice = 0; indice < listaDeInstructores.Count; indice = indice + 1)
            {
                if (listaDeInstructores[indice].Id == instructorEditado.Id)
                {
                    listaDeInstructores[indice] = instructorEditado;
                }
            }

            GuardarTodoEnCsv(listaDeInstructores);
        }

        public void Eliminar(int idDelInstructor)
        {
            List<Instructor> listaDeInstructores = Listar();
            List<Instructor> listaActualizada = new List<Instructor>();

            foreach(Instructor instructorActual in listaDeInstructores)
            {
                if (instructorActual.Id != idDelInstructor)
                {
                    listaActualizada.Add(instructorActual);
                }
            }

            GuardarTodoEnCsv(listaActualizada);
        }

        public Instructor BuscarPorId(int idDelInstructor)
        {
            List<Instructor> listaDeInstructores = Listar();
            foreach(Instructor instructorActual in listaDeInstructores)
            {
                if (instructorActual.Id == idDelInstructor)
                {
                    return instructorActual;
                }
            }
            return null; // Retorna nulo si no lo encuentra
        }

        private void GuardarTodoEnCsv(List<Instructor> listaParaGuardar)
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