using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SistemaCursosOnline.Models;
using SistemaCursosOnline.Interfaces;

namespace SistemaCursosOnline.Data
{
    // Una clase sencilla usada solo para guardar el Curso con sus modulos planos y no quebrar el programa. 
    public class CursoParaGuardar
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CupoMaximo { get; set; }
        public int InstructorId { get; set; }
        public string ListaDeModulosUnidaComoTexto { get; set; } // Ejemplo: "1:Backend:20|2:Frontend:10"
    }

    public class CursoRepository : ICrud<Curso>
    {
        private string rutaDelArchivo = "Data/cursos.csv";

        public CursoRepository()
        {
            if (Directory.Exists("Data") == false)
            {
                Directory.CreateDirectory("Data");
            }
        }

        public void Crear(Curso nuevoCurso)
        {
            List<Curso> listaDeCursos = Listar();
            listaDeCursos.Add(nuevoCurso);
            GuardarTodoEnCsv(listaDeCursos);
        }

        public List<Curso> Listar()
        {
            if (File.Exists(rutaDelArchivo) == false)
            {
                return new List<Curso>();
            }

            List<Curso> listaFinal = new List<Curso>();

            using (StreamReader lectorDeArchivos = new StreamReader(rutaDelArchivo))
            {
                CsvConfiguration configuracionCsv = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (CsvReader lectorCsv = new CsvReader(lectorDeArchivos, configuracionCsv))
                {
                    IEnumerable<CursoParaGuardar> registrosLeidos = lectorCsv.GetRecords<CursoParaGuardar>();
                    
                    // Transformar la clase "para guardar" de nuevo a la clase real Curso
                    foreach (CursoParaGuardar cursoLeido in registrosLeidos)
                    {
                        Curso cursoReal = new Curso();
                        cursoReal.Id = cursoLeido.Id;
                        cursoReal.Nombre = cursoLeido.Nombre;
                        cursoReal.Descripcion = cursoLeido.Descripcion;
                        cursoReal.CupoMaximo = cursoLeido.CupoMaximo;
                        cursoReal.InstructorId = cursoLeido.InstructorId;

                        // Desarmar la lista de modulos desde el texto
                        if (cursoLeido.ListaDeModulosUnidaComoTexto != null && cursoLeido.ListaDeModulosUnidaComoTexto != "")
                        {
                            string[] modulosEnTexto = cursoLeido.ListaDeModulosUnidaComoTexto.Split('|');
                            foreach (string textoUnico in modulosEnTexto)
                            {
                                if (textoUnico != "")
                                {
                                    string[] pedazosDelTexto = textoUnico.Split(':');
                                    Modulo moduloNuevo = new Modulo();
                                    moduloNuevo.Id = int.Parse(pedazosDelTexto[0]);
                                    moduloNuevo.Titulo = pedazosDelTexto[1];
                                    moduloNuevo.DuracionHoras = int.Parse(pedazosDelTexto[2]);
                                    moduloNuevo.CursoId = cursoReal.Id;

                                    cursoReal.AgregarModulo(moduloNuevo);
                                }
                            }
                        }

                        listaFinal.Add(cursoReal);
                    }
                }
            }

            return listaFinal;
        }

        public void Actualizar(Curso cursoEditado)
        {
            List<Curso> listaDeCursos = Listar();
            
            for (int indice = 0; indice < listaDeCursos.Count; indice = indice + 1)
            {
                if (listaDeCursos[indice].Id == cursoEditado.Id)
                {
                    listaDeCursos[indice] = cursoEditado;
                }
            }

            GuardarTodoEnCsv(listaDeCursos);
        }

        public void Eliminar(int idDelCurso)
        {
            List<Curso> listaDeCursos = Listar();
            List<Curso> listaActualizada = new List<Curso>();

            foreach(Curso cursoActual in listaDeCursos)
            {
                if (cursoActual.Id != idDelCurso)
                {
                    listaActualizada.Add(cursoActual);
                }
            }

            GuardarTodoEnCsv(listaActualizada);
        }

        public Curso BuscarPorId(int idDelCurso)
        {
            List<Curso> listaDeCursos = Listar();
            foreach(Curso cursoActual in listaDeCursos)
            {
                if (cursoActual.Id == idDelCurso)
                {
                    return cursoActual;
                }
            }
            return null; // Retorna nulo si no lo encuentra
        }

        public void GuardarTodoEnCsv(List<Curso> listaParaGuardar)
        {
            List<CursoParaGuardar> listaTransformada = new List<CursoParaGuardar>();
            
            foreach (Curso cursoReal in listaParaGuardar)
            {
                CursoParaGuardar archivoTransformado = new CursoParaGuardar();
                archivoTransformado.Id = cursoReal.Id;
                archivoTransformado.Nombre = cursoReal.Nombre;
                archivoTransformado.Descripcion = cursoReal.Descripcion;
                archivoTransformado.CupoMaximo = cursoReal.CupoMaximo;
                archivoTransformado.InstructorId = cursoReal.InstructorId;

                string textoFinal = "";
                foreach (Modulo moduloActual in cursoReal.ListaDeModulos)
                {
                    textoFinal = textoFinal + moduloActual.Id + ":" + moduloActual.Titulo + ":" + moduloActual.DuracionHoras + "|";
                }
                archivoTransformado.ListaDeModulosUnidaComoTexto = textoFinal;

                listaTransformada.Add(archivoTransformado);
            }

            using (StreamWriter escritorDeArchivos = new StreamWriter(rutaDelArchivo))
            {
                CsvConfiguration configuracionCsv = new CsvConfiguration(CultureInfo.InvariantCulture);
                using (CsvWriter escritorCsv = new CsvWriter(escritorDeArchivos, configuracionCsv))
                {
                    escritorCsv.WriteRecords(listaTransformada);
                }
            }
        }
    }
}