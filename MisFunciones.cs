using System.Security.Cryptography.X509Certificates;
using Boletin.Entities;
using Newtonsoft.Json;

namespace Boletin;

public class MisFunciones
{
    public static byte MenuNotas()
    {
        Console.WriteLine("1. Registro quices");
        Console.WriteLine("2. Registro trabajos");
        Console.WriteLine("3. Registro parciales");
        Console.WriteLine("0. Regresar al menu principal");
        Console.Write("Opcion: ");
        return Convert.ToByte(Console.ReadLine());
    }
    public static byte menuEditNotas()
    {
        Console.Clear();
        Console.WriteLine("EDITAR NOTAS");
        Console.WriteLine("1. Editar notas: ");
        Console.WriteLine("2. Eliminar notas: ");
        Console.WriteLine("0. Regresar al menu principal: ");
        Console.WriteLine("Opcion: ");
        return Convert.ToByte(Console.ReadLine());
    }
    public static byte Reportes()
    {
        Console.WriteLine("1. Notas del grupo");
        Console.WriteLine("2. Notas Finales");
        Console.WriteLine("0. Regresar al menu principal");
        Console.Write("Opcion: ");
        return Convert.ToByte(Console.ReadLine());
    }

    public static void SaveData(List<Estudiante> lstListado)
    {
        string json = JsonConvert.SerializeObject(lstListado, Formatting.Indented);
        File.WriteAllText("boletin.json", json);
    }
    public static List<Estudiante> LoadData()
    {
        using (StreamReader reader = new StreamReader("boletin.json"))
        {
            string json = reader.ReadToEnd();
            return System.Text.Json.JsonSerializer
            .Deserialize<List<Estudiante>>(json, new System.Text.Json.JsonSerializerOptions()
            { PropertyNameCaseInsensitive = true }) ?? new List<Estudiante>();
        }
    }
    public static void SaveDataEstudiantes(List<Estudiante> listaestudiantes, Estudiante estudianteNuevo)
    {
        if (listaestudiantes.FirstOrDefault(x => x.Code == estudianteNuevo.Code)== null)
        {
            Console.WriteLine("El estudiante ya se encuentra registrado en el Sistema...");
        }
        {
            listaestudiantes.Add(estudianteNuevo);
            String json = JsonConvert.SerializeObject(listaestudiantes, Formatting.Indented);
            File.WriteAllText("boletin.json", json);
        }
    }
    public static void ImprimirNotas()
    {
        int estudiantesPorPagina = 10;
        int paginaActual = 0;
        List<Estudiante> estudiantes = LoadData();
        while (paginaActual * estudiantesPorPagina < estudiantes.Count){
            Console.Clear();
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("| {0,-20} | {1,-40} | {2,-5} | {3,-5} | {4,-5} | {5,-5} | {6,-5} | {7,-5} | {8,-5} | {9,-5} | {10,-5} |", "Cod.Estudiante", "Nombre Estudiante", "Q1", "Q2", "Q3", "Q4", "T1", "T2", "P1", "P2", "P3");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------");
            for (int i = paginaActual * estudiantesPorPagina; i < (paginaActual + 1) * estudiantesPorPagina && i < estudiantes.Count; i++)
            {
                Estudiante estudiante = estudiantes[i];
                Console.Write("| {0,-20} | {1,-40} |", estudiante.Code, estudiante.Nombre);
                ImprimirNotasDeLista("Q", estudiante.Quices, 4);
                ImprimirNotasDeLista("T", estudiante.Trabajos, 2);
                ImprimirNotasDeLista("P", estudiante.Parciales, 3);
                Console.WriteLine();
            }
            Console.WriteLine("Página {0}", paginaActual + 1);
            Console.WriteLine("Presione Enter para continuar a la siguiente pagina...");
            Console.ReadKey();
            Console.Clear();
            var estudiantesPagina = estudiantes
            .Skip(paginaActual * estudiantesPorPagina)
            .Take(estudiantesPorPagina);
            Console.WriteLine("Presione Enter para avanzar a la siguiente página o cualquier otra tecla para salir...");
            if (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                break; //? Salir si el usuario no presiona "Enter" para avanzar.
            }
        paginaActual++;
        }
    }
    public static void ImprimirNotasDeLista(string tipo, List<float> notas, int cantidad)
    {
        for (int i = 0; i < cantidad; i ++)
        {
            float nota = notas.ElementAtOrDefault(i);
            Console.WriteLine(" {0,-5} |", nota != 0.0f ? nota.ToString() :  "0.0");
        }
    }
    public static float calcularNotasFinal(List<float> quices, List<float> trabajos, List<float> parciales)
    {
        float valorQuices  = 0.25f;
        float valorTrabajos = 0.15f;
        float valorParciales = 0.60f;
        float sumaQuices = quices.Take(4).DefaultIfEmpty(0.0f).Sum();
        float sumaTrabajos = trabajos.Take(2).DefaultIfEmpty(0.0f).Sum();
        float sumaParciales = parciales.Take(3).DefaultIfEmpty(0.0f).Sum();
        float notaFinal = (sumaQuices *  valorQuices) + (sumaTrabajos * valorTrabajos) + (sumaParciales * valorParciales);
        return notaFinal;
    }
    public static void ImprimirNotasFinales(List<Estudiante> estudiantes)
    {
        Console.Clear();
        Console.WriteLine("Tabla de notas finales..");
        Console.WriteLine("----------------------------------------------------------------------------------");
        Console.WriteLine("| {0, -20}| {1,-40} | {2,-7} |","Cod.Estudiante","Nombre estudiante ", "Nota final");
        Console.WriteLine("----------------------------------------------------------------------------------");
        foreach ( var estudiante in estudiantes)
        {
            float notaFinal = calcularNotasFinal(estudiante.Quices,estudiante.Trabajos,estudiante.Parciales);
            Console.WriteLine("| {0, -20}| {1,-40} | {2,-7} |", estudiante.Code, estudiante.Nombre, notaFinal.ToString());
        }
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Presione Enter para continuar...");
            Console.ReadKey();
    }
}
