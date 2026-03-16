using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SistemaCursosOnline.Models;
using SistemaCursosOnline.Interfaces;

namespace SistemaCursosOnline.Data
{
    public class InscripcionRepository : ICrud<Inscripcion>
    {
        private string rutaDelArchivo = "Data/inscripciones.csv";

        public InscripcionRepository()
        {
            if (Directory.Exists("Data") == false)
            {
                Directory.CreateDirectory("Data");
            }
        }

        public void Crear(Inscripcion nuevaInscripcion)
        {
            List<Inscripcion> listaDeInscripciones = Listar();
            listaDeInscripciones.Add(nuevaInscripcion);
            GuardarTodoEnCsv(listaDeInscripciones);
        }

        public List<Inscripcion> Listar()
        {
            if (File.Exists(rutaDelArchivo) == false)
            {
                return new List<Inscripcion>();
            }

            using (StreamReader lectorDeArchivos = new StreamReader(rutaDelArchivo))
            {
                CsvConfiguration configuracionCsv = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (CsvReader lectorCsv = new CsvReader(lectorDeArchivos, configuracionCsv))
                {
                    IEnumerable<Inscripcion> registrosLeidos = lectorCsv.GetRecords<Inscripcion>();
                    return new List<Inscripcion>(registrosLeidos);
                }
            }
        }

        public void Actualizar(Inscripcion inscripcionEditada)
        {
            List<Inscripcion> listaDeInscripciones = Listar();
            
            for (int indice = 0; indice < listaDeInscripciones.Count; indice = indice + 1)
            {
                if (listaDeInscripciones[indice].Id == inscripcionEditada.Id)
                {
                    listaDeInscripciones[indice] = inscripcionEditada;
                }
            }

            GuardarTodoEnCsv(listaDeInscripciones);
        }

        public void Eliminar(int idDeLaInscripcion)
        {
            List<Inscripcion> listaDeInscripciones = Listar();
            List<Inscripcion> listaActualizada = new List<Inscripcion>();

            foreach(Inscripcion inscripcionActual in listaDeInscripciones)
            {
                if (inscripcionActual.Id != idDeLaInscripcion)
                {
                    listaActualizada.Add(inscripcionActual);
                }
            }

            GuardarTodoEnCsv(listaActualizada);
        }

        public Inscripcion BuscarPorId(int idDeLaInscripcion)
        {
            List<Inscripcion> listaDeInscripciones = Listar();
            foreach(Inscripcion inscripcionActual in listaDeInscripciones)
            {
                if (inscripcionActual.Id == idDeLaInscripcion)
                {
                    return inscripcionActual;
                }
            }
            return null; // Retorna nulo si no lo encuentra
        }

        private void GuardarTodoEnCsv(List<Inscripcion> listaParaGuardar)
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