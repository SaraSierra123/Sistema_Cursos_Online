using System;
using System.Collections.Generic;
using SistemaCursosOnline.Models;
using SistemaCursosOnline.Data;

namespace SistemaCursosOnline
{
    class Program
    {
        // Instancias de los repositorios para guardar datos
        static EstudianteRepository repositorioDeEstudiantes = new EstudianteRepository();
        static InstructorRepository repositorioDeInstructores = new InstructorRepository();
        static CursoRepository repositorioDeCursos = new CursoRepository();
        static InscripcionRepository repositorioDeInscripciones = new InscripcionRepository();

        static void Main(string[] args)
        {
            bool salirDelPrograma = false;
            
            while (salirDelPrograma == false)
            {
                Console.WriteLine("\n=== SISTEMA DE GESTION DE CURSOS ONLINE ===");
                Console.WriteLine("1. Gestionar Estudiantes");
                Console.WriteLine("2. Gestionar Instructores");
                Console.WriteLine("3. Gestionar Cursos");
                Console.WriteLine("4. Gestionar Inscripciones");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opcion: ");

                string opcionElegida = Console.ReadLine();

                if (opcionElegida == "1") MenuEstudiantes();
                else if (opcionElegida == "2") MenuInstructores();
                else if (opcionElegida == "3") MenuCursos();
                else if (opcionElegida == "4") MenuInscripciones();
                else if (opcionElegida == "5") 
                {
                    salirDelPrograma = true;
                    Console.WriteLine("Guardando cambios y saliendo del sistema...");
                }
                else 
                {
                    Console.WriteLine("Opcion no valida.");
                }
            }
        }

        // ==========================================
        // MENU DE ESTUDIANTES 
        // ==========================================
        static void MenuEstudiantes()
        {
            bool volverAlPrincipal = false;
            while (volverAlPrincipal == false)
            {
                Console.WriteLine("\n--- CRUD Estudiantes ---");
                Console.WriteLine("1. Crear Estudiante");
                Console.WriteLine("2. Listar Estudiantes");
                Console.WriteLine("3. Actualizar Estudiante");
                Console.WriteLine("4. Eliminar Estudiante");
                Console.WriteLine("5. Volver");
                Console.Write("Opcion: ");
                string opcionDeEstudiantes = Console.ReadLine();

                if (opcionDeEstudiantes == "1")
                {
                    int nuevoId = 1;
                    foreach(Estudiante estudianteActual in repositorioDeEstudiantes.Listar())
                    {
                        if (estudianteActual.Id >= nuevoId) nuevoId = estudianteActual.Id + 1;
                    }

                    Console.Write("Nombre del Estudiante: ");
                    string nombreIngresado = Console.ReadLine();
                    Console.Write("Email: ");
                    string emailIngresado = Console.ReadLine();
                    Console.Write("Codigo del Estudiante: ");
                    string codigoIngresado = Console.ReadLine();

                    Estudiante nuevoEstudiante = new Estudiante();
                    nuevoEstudiante.Id = nuevoId;
                    nuevoEstudiante.Nombre = nombreIngresado;
                    nuevoEstudiante.Email = emailIngresado;
                    nuevoEstudiante.CodigoEstudiante = codigoIngresado;

                    repositorioDeEstudiantes.Crear(nuevoEstudiante);
                    Console.WriteLine("Estudiante creado.");
                }
                else if (opcionDeEstudiantes == "2")
                {
                    Console.WriteLine("\n--- Lista de Estudiantes ---");
                    foreach (Estudiante estudianteActual in repositorioDeEstudiantes.Listar())
                    {
                        Console.WriteLine("ID: " + estudianteActual.Id + " | Nombre: " + estudianteActual.Nombre + " | Codigo: " + estudianteActual.CodigoEstudiante);
                    }
                }
                else if (opcionDeEstudiantes == "3")
                {
                    Console.Write("Ingrese el ID del estudiante a editar: ");
                    int idParaEditar = int.Parse(Console.ReadLine());
                    Estudiante estudianteEncontrado = repositorioDeEstudiantes.BuscarPorId(idParaEditar);

                    if (estudianteEncontrado != null)
                    {
                        Console.Write("Nuevo Nombre: ");
                        estudianteEncontrado.Nombre = Console.ReadLine();
                        Console.Write("Nuevo Email: ");
                        estudianteEncontrado.Email = Console.ReadLine();
                        
                        repositorioDeEstudiantes.Actualizar(estudianteEncontrado);
                        Console.WriteLine("Estudiante actualizado.");
                    }
                    else Console.WriteLine("Estudiante no encontrado.");
                }
                else if (opcionDeEstudiantes == "4")
                {
                    Console.Write("Ingrese el ID del estudiante a eliminar: ");
                    int idParaBorrar = int.Parse(Console.ReadLine());
                    repositorioDeEstudiantes.Eliminar(idParaBorrar);
                    Console.WriteLine("Borrado ejecutado (si existia).");
                }
                else if (opcionDeEstudiantes == "5")
                {
                    volverAlPrincipal = true;
                }
            }
        }

        // ==========================================
        // MENU DE INSTRUCTORES
        // ==========================================
        static void MenuInstructores()
        {
            bool volverAlPrincipal = false;
            while (volverAlPrincipal == false)
            {
                Console.WriteLine("\n--- CRUD Instructores ---");
                Console.WriteLine("1. Crear Instructor");
                Console.WriteLine("2. Listar Instructores");
                Console.WriteLine("3. Actualizar Instructor");
                Console.WriteLine("4. Eliminar Instructor");
                Console.WriteLine("5. Volver");
                Console.Write("Opcion: ");
                string opcionDeInstructores = Console.ReadLine();

                if (opcionDeInstructores == "1")
                {
                    int nuevoId = 1;
                    foreach(Instructor instructorActual in repositorioDeInstructores.Listar())
                    {
                        if (instructorActual.Id >= nuevoId) nuevoId = instructorActual.Id + 1;
                    }

                    Console.Write("Nombre del Instructor: ");
                    string nombreIngresado = Console.ReadLine();
                    Console.Write("Email: ");
                    string emailIngresado = Console.ReadLine();
                    Console.Write("Especialidad: ");
                    string especialidadIngresada = Console.ReadLine();

                    Instructor nuevoInstructor = new Instructor();
                    nuevoInstructor.Id = nuevoId;
                    nuevoInstructor.Nombre = nombreIngresado;
                    nuevoInstructor.Email = emailIngresado;
                    nuevoInstructor.Especialidad = especialidadIngresada;

                    repositorioDeInstructores.Crear(nuevoInstructor);
                    Console.WriteLine("Instructor creado.");
                }
                else if (opcionDeInstructores == "2")
                {
                    Console.WriteLine("\n--- Lista de Instructores ---");
                    foreach (Instructor instructorActual in repositorioDeInstructores.Listar())
                    {
                        Console.WriteLine("ID: " + instructorActual.Id + " | Nombre: " + instructorActual.Nombre + " | Especialidad: " + instructorActual.Especialidad);
                    }
                }
                else if (opcionDeInstructores == "3")
                {
                    Console.Write("Ingrese el ID del instructor a editar: ");
                    int idParaEditar = int.Parse(Console.ReadLine());
                    Instructor instructorEncontrado = repositorioDeInstructores.BuscarPorId(idParaEditar);

                    if (instructorEncontrado != null)
                    {
                        Console.Write("Nueva Especialidad: ");
                        instructorEncontrado.Especialidad = Console.ReadLine();
                        
                        repositorioDeInstructores.Actualizar(instructorEncontrado);
                        Console.WriteLine("Instructor actualizado.");
                    }
                    else Console.WriteLine("Instructor no encontrado.");
                }
                else if (opcionDeInstructores == "4")
                {
                    Console.Write("Ingrese el ID del instructor a eliminar: ");
                    int idParaBorrar = int.Parse(Console.ReadLine());
                    repositorioDeInstructores.Eliminar(idParaBorrar);
                    Console.WriteLine("Borrado ejecutado.");
                }
                else if (opcionDeInstructores == "5")
                {
                    volverAlPrincipal = true;
                }
            }
        }

        // ==========================================
        // MENU DE CURSOS Y MODULOS
        // ==========================================
        static void MenuCursos()
        {
            bool volverAlPrincipal = false;
            while (volverAlPrincipal == false)
            {
                Console.WriteLine("\n--- CRUD Cursos ---");
                Console.WriteLine("1. Crear Curso");
                Console.WriteLine("2. Listar Cursos con sus Modulos");
                Console.WriteLine("3. Actualizar Datos del Curso");
                Console.WriteLine("4. Agregar un Modulo al Curso");
                Console.WriteLine("5. Eliminar Curso");
                Console.WriteLine("6. Volver");
                Console.Write("Opcion: ");
                string opcionDeCursos = Console.ReadLine();

                if (opcionDeCursos == "1")
                {
                    int nuevoId = 1;
                    foreach(Curso cursoActual in repositorioDeCursos.Listar())
                    {
                        if (cursoActual.Id >= nuevoId) nuevoId = cursoActual.Id + 1;
                    }

                    Console.Write("Nombre del Curso: ");
                    string nombreIngresado = Console.ReadLine();
                    Console.Write("Descripcion corta: ");
                    string descripcionIngresada = Console.ReadLine();
                    Console.Write("Cupo de estudiantes permitidos: ");
                    int cupoIngresado = int.Parse(Console.ReadLine());

                    Console.WriteLine("\n--- Instructores Disponibles ---");
                    foreach (Instructor instructorActual in repositorioDeInstructores.Listar())
                    {
                        Console.WriteLine("ID Instructor: " + instructorActual.Id + " | Especialidad: " + instructorActual.Especialidad);
                    }
                    Console.Write("Ingrese un ID valido del Instructor del Curso: ");
                    int instructorIdIngresado = int.Parse(Console.ReadLine());

                    Curso nuevoCurso = new Curso();
                    nuevoCurso.Id = nuevoId;
                    nuevoCurso.Nombre = nombreIngresado;
                    nuevoCurso.Descripcion = descripcionIngresada;
                    nuevoCurso.CupoMaximo = cupoIngresado;
                    nuevoCurso.InstructorId = instructorIdIngresado;

                    repositorioDeCursos.Crear(nuevoCurso);
                    Console.WriteLine("Curso creado de forma exitosa.");
                }
                else if (opcionDeCursos == "2")
                {
                    Console.WriteLine("\n--- Lista de Cursos ---");
                    foreach (Curso cursoActual in repositorioDeCursos.Listar())
                    {
                        Console.WriteLine("ID: " + cursoActual.Id + " | Nombre: " + cursoActual.Nombre + " | Docente Asignado (ID): " + cursoActual.InstructorId);
                        Console.WriteLine(" - Modulos del curso:");
                        
                        if (cursoActual.ListaDeModulos.Count == 0)
                        {
                            Console.WriteLine("   Sin modulos");
                        }
                        
                        foreach(Modulo moduloActual in cursoActual.ListaDeModulos)
                        {
                            Console.WriteLine("   + " + moduloActual.Titulo + " (" + moduloActual.DuracionHoras + " hrs)");
                        }
                    }
                }
                else if (opcionDeCursos == "3")
                {
                    Console.Write("Ingrese el ID del curso a editar: ");
                    int idParaEditar = int.Parse(Console.ReadLine());
                    Curso cursoEncontrado = repositorioDeCursos.BuscarPorId(idParaEditar);

                    if (cursoEncontrado != null)
                    {
                        Console.Write("Nuevo Nombre: ");
                        cursoEncontrado.Nombre = Console.ReadLine();
                        
                        repositorioDeCursos.Actualizar(cursoEncontrado);
                        Console.WriteLine("Curso actualizado.");
                    }
                    else Console.WriteLine("Curso no encontrado.");
                }
                else if (opcionDeCursos == "4")
                {
                    Console.Write("Ingrese el ID del curso al que le insertara un modulo: ");
                    int idDelCurso = int.Parse(Console.ReadLine());
                    Curso cursoAfectado = repositorioDeCursos.BuscarPorId(idDelCurso);

                    if (cursoAfectado != null)
                    {
                        Console.Write("Titulo del modulo: ");
                        string tituloDelModulo = Console.ReadLine();
                        Console.Write("Duracion en Horas: ");
                        int horas = int.Parse(Console.ReadLine());

                        int nuevoIdModulo = 1;
                        foreach(Modulo moduloActual in cursoAfectado.ListaDeModulos)
                        {
                            if (moduloActual.Id >= nuevoIdModulo)
                            {
                                nuevoIdModulo = moduloActual.Id + 1;
                            }
                        }

                        Modulo moduloNuevo = new Modulo();
                        moduloNuevo.Id = nuevoIdModulo;
                        moduloNuevo.Titulo = tituloDelModulo;
                        moduloNuevo.DuracionHoras = horas;
                        moduloNuevo.CursoId = cursoAfectado.Id;

                        cursoAfectado.AgregarModulo(moduloNuevo);
                        repositorioDeCursos.Actualizar(cursoAfectado); // Guarda el curso con su nueva composicion

                        Console.WriteLine("Modulo guardado y asimilado en el curso de forma permanente.");
                    }
                    else Console.WriteLine("Curso invalido.");
                }
                else if (opcionDeCursos == "5")
                {
                    Console.Write("Ingrese el ID del curso a eliminar: ");
                    int idParaBorrar = int.Parse(Console.ReadLine());
                    repositorioDeCursos.Eliminar(idParaBorrar);
                    Console.WriteLine("Borrado ejecutado (los modulos tambien mueren con el curso por Composicion).");
                }
                else if (opcionDeCursos == "6")
                {
                    volverAlPrincipal = true;
                }
            }
        }

        // ==========================================
        // MENU DE INSCRIPCIONES (ASOCIACION)
        // ==========================================
        static void MenuInscripciones()
        {
            bool volverAlPrincipal = false;
            while (volverAlPrincipal == false)
            {
                Console.WriteLine("\n--- CRUD Inscripciones ---");
                Console.WriteLine("1. Crear Inscripcion (Matricular Alumno)");
                Console.WriteLine("2. Listar Matriculas");
                Console.WriteLine("3. Eliminar Inscripcion");
                Console.WriteLine("4. Volver");
                Console.Write("Opcion: ");
                string opcionDeInscripciones = Console.ReadLine();

                if (opcionDeInscripciones == "1")
                {
                    int nuevoId = 1;
                    foreach(Inscripcion inscripcionActual in repositorioDeInscripciones.Listar())
                    {
                        if (inscripcionActual.Id >= nuevoId) nuevoId = inscripcionActual.Id + 1;
                    }

                    Console.WriteLine("--- Estudiantes Registrados ---");
                    foreach (Estudiante alumnoActual in repositorioDeEstudiantes.Listar())
                    {
                        Console.WriteLine("ID: " + alumnoActual.Id + " | " + alumnoActual.Nombre);
                    }
                    Console.Write("Escriba el ID del Estudiante a inscribir: ");
                    int idEstudianteIngresado = int.Parse(Console.ReadLine());

                    System.Console.WriteLine();
                    Console.WriteLine("--- Cursos Disponibles ---");
                    foreach (Curso cursoVigente in repositorioDeCursos.Listar())
                    {
                        Console.WriteLine("ID: " + cursoVigente.Id + " | " + cursoVigente.Nombre);
                    }
                    Console.Write("Escriba el ID del Curso para matricular: ");
                    int idCursoIngresado = int.Parse(Console.ReadLine());

                    Console.Write("Fecha y Hora de la inscripcion: ");
                    string fechaIngresada = Console.ReadLine();

                    Inscripcion matricula = new Inscripcion();
                    matricula.Id = nuevoId;
                    matricula.EstudianteId = idEstudianteIngresado;
                    matricula.CursoId = idCursoIngresado;
                    matricula.FechaInscripcion = fechaIngresada;

                    repositorioDeInscripciones.Crear(matricula);
                    Console.WriteLine("El estudiante ahora esta asociado al curso.");
                }
                else if (opcionDeInscripciones == "2")
                {
                    Console.WriteLine("\n--- Lista de Inscripciones Activas ---");
                    foreach (Inscripcion matriculaActual in repositorioDeInscripciones.Listar())
                    {
                        Console.WriteLine("Matricula ID: " + matriculaActual.Id + " | Alumno #" + matriculaActual.EstudianteId + " <---> Curso #" + matriculaActual.CursoId + " | En fecha: " + matriculaActual.FechaInscripcion);
                    }
                }
                else if (opcionDeInscripciones == "3")
                {
                    Console.Write("Ingrese el ID de la matricula a remover: ");
                    int idParaBorrar = int.Parse(Console.ReadLine());
                    repositorioDeInscripciones.Eliminar(idParaBorrar);
                    Console.WriteLine("Inscripcion borrada. El alumno ya no cursara esta materia.");
                }
                else if (opcionDeInscripciones == "4")
                {
                    volverAlPrincipal = true;
                }
            }
        }
    }
}