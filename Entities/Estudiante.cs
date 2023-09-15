using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Boletin.Entities
{
    public class Estudiante : Boletin
    {
        private string code;
        private string nombre;
        private string direccion;
        private byte edad;



        public Estudiante() : base()
        {
        }

        public Estudiante(List<float> quices, List<float> trabajos, List<float> parciales, string code, string nombre, string direccion, byte edad) : base(quices, trabajos, parciales)
        {
            this.Code = code;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Edad = edad;
            this.Quices = quices;
            this.Trabajos = trabajos;
            this.Parciales = parciales;
        }

        public string Code { get => code; set => code = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public byte Edad { get => edad; set => edad = value; }
        public void InfoEstudiante(List<Estudiante> estudiantes)
        {
            Estudiante estudiante = new Estudiante();
            Console.Write("Codigo: ");
            estudiante.Code = Console.ReadLine();
            Console.Write("Nombre: ");
            estudiante.Nombre = Console.ReadLine();
            Console.Write("Direccion: ");
            estudiante.Direccion = Console.ReadLine();
            Console.Write("Edad: ");
            estudiante.Edad = Convert.ToByte(Console.ReadLine());
            estudiante.Quices = new List<float>();
            estudiante.Trabajos = new List<float>();
            estudiante.Parciales = new List<float>();
            estudiantes.Add(estudiante);
        }

        public void RegistroNota(List<Estudiante> estudiantes, int opcion)
        {
            Console.WriteLine("Ingrese el codigo del estudiante: ");
            string studenCode = Console.ReadLine();
            Estudiante alumno = estudiantes.FirstOrDefault(x => x.Code.Equals(studenCode));
            Console.WriteLine("Por favor ingrese la nota del : ");
            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Quiz Nro : {0}", (alumno.Quices.Count + 1));
                    alumno.Quices.Add(float.Parse(Console.ReadLine()));
                    break;
                case 2:
                    alumno.Trabajos.Add(float.Parse(Console.ReadLine()));
                    break;
                case 3:
                    alumno.Parciales.Add(float.Parse(Console.ReadLine()));
                    break;
                default:
                    Console.WriteLine("Opcion no valida");
                    Console.ReadKey();
                    break;
            }
            int idx = estudiantes.FindIndex(p => p.Code.Equals(studenCode));
            estudiantes[idx] = alumno;
        }
        public void RemoveItem(List<Estudiante> estudiantes){
            Console.Clear();
            Console.WriteLine("Ingrese el Codigo del Estudiante a Eliminar");
            string id = Console.ReadLine();
            Estudiante studentToRemove = estudiantes.FirstOrDefault(st => (st.Code ?? string.Empty)
            .Equals(id)) ?? new Estudiante();
            if (studentToRemove != null)
            {
                estudiantes.Remove(studentToRemove);
                MisFunciones.SaveData(estudiantes);
            }
        }
        
        public void validarEstudiante(List<Estudiante> estudiantes)
    {
        Estudiante estudiante = new Estudiante();
        Console.Write("Codigo: ");
        string codigo = Console.ReadLine();

        //! Validar si el código ya existe en la lista
        if (estudiantes.Any(e => e.Code == codigo))
        {
            Console.WriteLine("El código de estudiante ya existe. Introduce un código único.");
        }
        else
        {
            estudiante.Code = codigo;
            Console.Write("Nombre: ");
            estudiante.Nombre = Console.ReadLine();
            Console.Write("Direccion: ");
            estudiante.Direccion = Console.ReadLine();
            Console.Write("Edad: ");
            estudiante.Edad = Convert.ToByte(Console.ReadLine());
            estudiante.Quices = new List<float>();
            estudiante.Trabajos = new List<float>();
            estudiante.Parciales = new List<float>();
            estudiantes.Add(estudiante);
        }
    }
    public void RemoveGrade(List<Estudiante> estudiantes){
            Console.Clear();
            Console.WriteLine("Ingrese el Codigo del Estudiante a Modificar");
            string id = Console.ReadLine();
            Estudiante studentToEdit = estudiantes.FirstOrDefault(st => (st.Code ?? string.Empty)
            .Equals(id)) ?? new Estudiante();
            if (studentToEdit != null)
            {
                Console.WriteLine("Ingrese el tipo de nota a eliminar");
                Console.WriteLine("1. Quiz\n2. Trabajo\n3. Parcial\n0. Salir");
                byte opcion = Convert.ToByte(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el numero de quiz a eliminar");
                        int quiz = Convert.ToInt32(Console.ReadLine());
                        if (quiz > studentToEdit.Quices.Count)
                        {
                            Console.WriteLine("El estudiante no tiene ese quiz");
                            Console.ReadKey();
                            break;
                        }else{
                            studentToEdit.Quices.RemoveAt(quiz - 1);
                            break;
                        }
                    case 2:
                        Console.WriteLine("Ingrese el numero de trabajo a modificar");
                        int trabajo = Convert.ToInt32(Console.ReadLine());
                        if (trabajo > studentToEdit.Trabajos.Count)
                        {
                            Console.WriteLine("El estudiante no tiene ese trabajo");
                            Console.ReadKey();
                            break;
                        }else{
                            studentToEdit.Trabajos.RemoveAt(trabajo - 1);
                            break;
                        }
                    case 3:
                        Console.WriteLine("Ingrese el numero de parcial a modificar");
                        int parcial = Convert.ToInt32(Console.ReadLine());
                        if (parcial > studentToEdit.Parciales.Count)
                        {
                            Console.WriteLine("El estudiante no tiene ese parcial");
                            Console.ReadKey();
                            break;
                        }else{
                            studentToEdit.Parciales.RemoveAt(parcial - 1);
                            break;
                        }
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opcion no valida");
                        Console.ReadKey();
                        break;
                }
                int idx = estudiantes.FindIndex(p => p.Code.Equals(id));
                estudiantes[idx] = studentToEdit;
                MisFunciones.SaveData(estudiantes);
            }
        }
    public void FindByCode(List<Estudiante> estudiantes){
        Console.Clear();
        Console.WriteLine("Ingrese el Codigo del Estudiante a Buscar");
        string id = Console.ReadLine();
        Estudiante studentToFind = estudiantes.FirstOrDefault(st => (st.Code ?? string.Empty)
        .Equals(id)) ?? new Estudiante();
        if (studentToFind != null)
        {
            Console.WriteLine("Codigo: {0}", studentToFind.Code);
            Console.WriteLine("Nombre: {0}", studentToFind.Nombre);
            Console.WriteLine("Edad: {0}", studentToFind.Edad);
            Console.WriteLine("Direccion: {0}", studentToFind.Direccion);
            Console.ReadKey();
        }
    }
    public void FindByName(List<Estudiante> estudiantes){
        Console.Clear();
        Console.WriteLine("Ingrese el Nombre del Estudiante a Buscar");
        string name = Console.ReadLine().ToUpper();
        Estudiante studentToFind = estudiantes.FirstOrDefault(st => (st.Nombre ?? string.Empty)
        .Equals(name)) ?? new Estudiante();
        if (studentToFind != null)
        {
            Console.WriteLine("Codigo: {0}", studentToFind.Code);
            Console.WriteLine("Nombre: {0}", studentToFind.Nombre);
            Console.WriteLine("Edad: {0}", studentToFind.Edad);
            Console.WriteLine("Direccion: {0}", studentToFind.Direccion);;
            Console.ReadKey();
        }
    }
    public void FindByAge(List<Estudiante> estudiantes){
        List<Estudiante> studentsToFind = new List<Estudiante>();
        Console.Clear();
        Console.WriteLine("Ingrese la Edad del Estudiante a Buscar");
        byte age = Convert.ToByte(Console.ReadLine());
        estudiantes.ForEach(st => {
            if (st.Edad == age)
            {
                studentsToFind.Add(st);
            }
        });
        if (studentsToFind.Count > 0)
        {
            studentsToFind.ForEach(st => {
                Console.WriteLine("Codigo: {0}", st.Code);
                Console.WriteLine("Nombre: {0}", st.Nombre);
                Console.WriteLine("Edad: {0}", st.Edad);
                Console.WriteLine("Direccion: {0}", st.Direccion);;
            });
            Console.ReadKey();
        }
    }
    public void FindByAddres(List<Estudiante> estudiantes){
        List<Estudiante> studentsToFind = new List<Estudiante>();
        Console.Clear();
        Console.WriteLine("Ingrese la Direccion del Estudiante a Buscar");
        string address = Console.ReadLine().ToUpper();
        estudiantes.ForEach(st => {
            if (st.Direccion == address)
            {
                studentsToFind.Add(st);
            }
        });
        if (studentsToFind.Count > 0)
        {
            studentsToFind.ForEach(st => {
                Console.WriteLine("Codigo: {0}", st.Code);
                Console.WriteLine("Nombre: {0}", st.Nombre);
                Console.WriteLine("Edad: {0}", st.Edad);
                Console.WriteLine("Direccion: {0}", st.Direccion);
                Console.WriteLine("Quices: {0}", st.Quices);
                Console.WriteLine("Trabajos: {0}", st.Trabajos);
                Console.WriteLine("Parciales: {0}", st.Parciales);
            });
            Console.ReadKey();
        }
    }
    public void EditGrades(List<Estudiante> estudiantes){
            Console.Clear();
            Console.WriteLine("Ingrese el Codigo del Estudiante a Modificar");
            string id = Console.ReadLine();
            Estudiante studentToEdit = estudiantes.FirstOrDefault(st => (st.Code ?? string.Empty)
            .Equals(id)) ?? new Estudiante();
            if (studentToEdit != null)
            {
                Console.WriteLine("Ingrese el tipo de nota a modificar");
                Console.WriteLine("1. Quiz\n2. Trabajo\n3. Parcial\n0. Salir");
                Console.WriteLine("Ingrese la nueva nota");
                float nota = float.Parse(Console.ReadLine());
                byte opcion = Convert.ToByte(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Ingrese el numero de quiz a modificar");
                        int quiz = Convert.ToInt32(Console.ReadLine());
                        if (quiz > studentToEdit.Quices.Count)
                        {
                            Console.WriteLine("El estudiante no tiene ese quiz");
                            Console.ReadKey();
                            break;
                        }else{
                            studentToEdit.Quices[quiz - 1] = nota;
                        break;
                        }
                        
                    case 2:
                        Console.WriteLine("Ingrese el numero de trabajo a modificar");
                        int trabajo = Convert.ToInt32(Console.ReadLine());
                        if (trabajo > studentToEdit.Trabajos.Count)
                        {
                            Console.WriteLine("El estudiante no tiene ese trabajo");
                            Console.ReadKey();
                            break;
                        }else{
                            studentToEdit.Trabajos[trabajo - 1] = nota;
                            break;
                        }
                    case 3:
                        Console.WriteLine("Ingrese el numero de parcial a modificar");
                        int parcial = Convert.ToInt32(Console.ReadLine());
                        if (parcial > studentToEdit.Parciales.Count)
                        {
                            Console.WriteLine("El estudiante no tiene ese parcial");
                            Console.ReadKey();
                            break;
                        }else{
                            studentToEdit.Parciales[parcial - 1] = nota;
                            break;
                        }
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opcion no valida");
                        Console.ReadKey();
                        break;
                }
                int idx = estudiantes.FindIndex(p => p.Code.Equals(id));
                estudiantes[idx] = studentToEdit;
                MisFunciones.SaveData(estudiantes);
            }
        }
    }

}