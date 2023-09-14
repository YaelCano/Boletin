﻿using Boletin;
using Boletin.Entities;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Estudiante> estudiantes = new List<Estudiante>();
        Estudiante student = new Estudiante();
        bool cicloMenu = true;
        estudiantes = MisFunciones.LoadData();
        while (cicloMenu)
        {
            Console.Clear();
            Console.WriteLine("1. Registro de estudiantes");
            Console.WriteLine("2. Registro de notas");
            Console.WriteLine("3. Reportes e informes");
            Console.WriteLine("4. Eliminar Alumno");
            Console.WriteLine("0. Salir");
            Console.Write("Opcion: ");
            byte opcionMenu = Convert.ToByte(Console.ReadLine());
            switch (opcionMenu)
            {
                case 1:
                    student.validarEstudiante(estudiantes);
                    student.InfoEstudiante(estudiantes);
                    MisFunciones.SaveData(estudiantes);
                    break;
                case 2:
                    bool cicloNotas = true;
                    while (cicloNotas)
                    {
                        Console.Clear();
                        byte opcionNotas = MisFunciones.MenuNotas();
                        if (opcionNotas != 0)
                        {
                            student.RegistroNota(estudiantes, opcionNotas);
                            MisFunciones.SaveData(estudiantes);
                        }
                        else
                        {
                            cicloNotas = false;
                        }
                    }

                    break;
                case 3:
                    bool cicloReportes = true;
                    while (cicloReportes)
                    {
                        Console.Clear();
                        byte opcionRegistro = MisFunciones.Reportes();
                        switch (opcionRegistro)
                        {
                            case 1:
                                Console.Clear();
                                break;
                            case 2:
                                Console.Clear();
                                break;
                            case 0:
                                Console.Clear();
                                cicloReportes = false;
                                break;
                            default:
                                Console.WriteLine("Opcion invalida");
                                Console.Write("Presione Enter para volver a ingresar: ");
                                Console.ReadKey();
                                break;
                        }
                    }
                    break;
                case 4:
                    student.RemoveItem(estudiantes);
                    break;
                case 0:
                    cicloMenu = false;
                    break;
                default:
                    Console.WriteLine("Opcion invalida");
                    Console.Write("Presione Enter para volver a ingresar: ");
                    Console.ReadKey();
                    break;
            }

        }


    }
}