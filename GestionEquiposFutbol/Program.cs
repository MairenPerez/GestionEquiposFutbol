using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEquiposFutbol
{
    internal class Program
    {
        static Dictionary<string, int> equiposFutbol = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            AlmacenarDatos();

            while (true)
            {
                Console.WriteLine(@"Gestión de equipos de fútbol
                                      1. Dar de alta un equipo
                                      2. Dar de baja un equipo
                                      3. Modificar puntuación
                                      4. Mostrar clasificación
                                      5. Salir y guardar cambios");

                Console.Write("Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out int opcion))
                {
                    Console.WriteLine("Opción inválida. Ingresa un número del 1 al 5.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        DarDeAlta();
                        break;
                    case 2:
                        DarDeBaja();
                        break;
                    case 3:
                        ModificarPuntuacion();
                        break;
                    case 4:
                        MostrarClasificacion();
                        break;
                    case 5:
                        GuardarDatos("equipos.txt");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Intenta nuevamente.");
                        break;
                }
            }
        }

        /// <summary>
        /// Damos de alta un equipo en la clasificación
        /// 
        /// Comprobamos si existe con la clave / valor
        /// Si existe, añadimos nuevo equipo.
        /// </summary>
        static void DarDeAlta()
        {
            Console.Write("Ingrese el nombre del equipo: ");
            string equipo = Console.ReadLine();

            if (equiposFutbol.ContainsKey(equipo))
            {
                Console.WriteLine("El equipo ya existe.");
            }
            else
            {
                Console.Write("Ingrese la puntuación inicial: ");
                if (int.TryParse(Console.ReadLine(), out int puntuacion))
                {
                    equiposFutbol.Add(equipo, puntuacion);
                    Console.WriteLine($"El equipo {equipo} ha sido dado de alta con {puntuacion} puntos.");
                }
                else
                {
                    Console.WriteLine("Puntuación inválida.");
                }
            }

            GuardarDatos("equipos.txt");
        }

        /// <summary>
        /// Damos de baja un equipo de la clasificación.
        /// 
        /// Comprobamos si existe con la clave / valor
        /// Si existe, se elimi con el método Remove
        /// </summary>
        static void DarDeBaja()
        {
            Console.Write("Ingrese el nombre del equipo a eliminar: ");
            string equipo = Console.ReadLine();

            if (equiposFutbol.Remove(equipo))
            {
                Console.WriteLine($"El equipo {equipo} ha sido eliminado.");
            }
            else
            {
                Console.WriteLine("El equipo no existe.");
            }

            GuardarDatos("equipos.txt");
        }


        /// <summary>
        /// Modificamos la puntuación de un equipo
        /// 
        /// Comprobamos si existe con la clave / valor
        /// Guardamos los datos.
        /// </summary>
        static void ModificarPuntuacion()
        {
            Console.Write("Ingrese el nombre del equipo a modificar: ");
            string equipo = Console.ReadLine();

            if (equiposFutbol.TryGetValue(equipo, out int puntuacionActual))
            {
                Console.Write("Ingrese la nueva puntuación: ");
                if (int.TryParse(Console.ReadLine(), out int nuevaPuntuacion))
                {
                    equiposFutbol[equipo] = nuevaPuntuacion;
                    Console.WriteLine($"La puntuación de {equipo} se ha actualizado a {nuevaPuntuacion}.");
                }
                else
                {
                    Console.WriteLine("Puntuación inválida.");
                }
            }
            else
            {
                Console.WriteLine("El equipo no existe.");
            }

            GuardarDatos("equipos.txt");
        }


        /// <summary>
        /// Mostramos la clasificación de los equipos
        /// </summary>
        static void MostrarClasificacion()
        {
            Console.WriteLine("Clasificación:");
            foreach (KeyValuePair<string, int> equipo in equiposFutbol.OrderByDescending(e => e.Value))
            {
                Console.WriteLine($"{equipo.Key}: {equipo.Value} puntos");
            }
        }

        public static void AlmacenarDatos()
        {
            equiposFutbol["Real Madrid"] = 92;
            equiposFutbol["Barcelona"] = 88;
            equiposFutbol["Atlético de Madrid"] = 85;
            equiposFutbol["Sevilla"] = 78;
            equiposFutbol["Villarreal"] = 75;
            equiposFutbol["Real Sociedad"] = 66;
            equiposFutbol["Granada"] = 55;
        }


        /// <summary>
        /// Guardamos los datos en un archivo de texto
        /// </summary>
        /// <param name="path"></param>
        public static void GuardarDatos(string path)
        {
            List<string> lineas = new List<string>();

            foreach (KeyValuePair<string, int> equipo in equiposFutbol)
            {
                lineas.Add($"{equipo.Key};{equipo.Value}");
            }

            File.WriteAllLines(path, lineas);
        }
    }
}
