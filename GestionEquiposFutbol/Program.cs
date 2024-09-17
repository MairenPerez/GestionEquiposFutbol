using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GestionEquiposFutbol
{
    internal class Program
    {
        //inicicialización e instanciación de las variables
        //carpeta solución del proyecto para no tener que poner cada una nuestra ruta relativa
        static string Ruta = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Ficheros");
        static string FicheroEquipos = "equipos.txt";
        static string FicheroJugadores = "jugadores.txt";

        static Dictionary<string, int> equiposFutbol = new Dictionary<string, int>()
        {
            { "FC Barcelona", 24 },
            { "Betis", 12 }
        };

        static Dictionary<string, string> jugdores = new Dictionary<string, string>()
        {
            { "Lionel Messi", "FC Barcelona" },
            { "Juan Pepe" , "Betis" }
        };

        static void Main(string[] args)
        {
            GuardarDatos();
            GuardarJugadores();
            LeerDatosEquipos();
            LeerDatosJugadores();

            while (true)
            {
                Console.WriteLine(@"
Gestión de equipos de fútbol
1. Dar de alta un equipo
2. Dar de baja un equipo
3. Modificar puntuación
4. Mostrar clasificación

5. Dar de alta un jugador
6. Dar de baja un jugador
7. Modificar equipo de jugador
8. Mostrar jugadores

0. Salir y guardar cambios
");

                Console.Write("Seleccione una opción: ");
                Console.WriteLine();
                if (!int.TryParse(Console.ReadLine(), out int opcion))
                {
                    Console.WriteLine("Opción inválida. Ingresa un número del 1 al 5.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        DarDeAlta();
                        LeerDatosEquipos();
                        break;
                    case 2:
                        DarDeBaja();
                        LeerDatosEquipos();
                        break;
                    case 3:
                        ModificarPuntuacion();
                        LeerDatosEquipos();
                        break;
                    case 4:
                        MostrarClasificacion();
                        break;
                    case 5:
                        DarAltaJug();
                        LeerDatosJugadores();
                        break;
                    case 6:
                        DarBajaJugador();
                        LeerDatosJugadores();
                        break;
                    case 7:
                        ModificarEquipoJug();
                        LeerDatosJugadores();
                        break;
                    case 8:
                        MostrarJugadores();
                        break;
                    case 0:
                        GuardarDatos();
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Intenta nuevamente.");
                        break;
                }
            }
        }

        /// <summary>
        /// Listamos todos jugadores con su equipo
        /// </summary>
        private static void MostrarJugadores()
        {
            Console.WriteLine("Lista de jugadores:");

            foreach (KeyValuePair<string, string> jugador in jugdores.OrderByDescending(j => j.Value))
                Console.WriteLine($"{jugador.Key}: {jugador.Value}");
        }

        /// <summary>
        /// Modificamos el equipo de un jugador.
        /// 
        /// Intentamos obtener el jugador con TryGetValue
        /// si existe, mostramos el equipo actual y solicitamos el nuevo equipo.
        /// </summary> 
        private static void ModificarEquipoJug()
        {
            Console.WriteLine("Ingresa el nombre jugador: ");
            Console.WriteLine();
            string jugador = Console.ReadLine();

            if (jugdores.TryGetValue(jugador, out string equipoActual))
            {
                Console.WriteLine($"El jugador {jugador} está actualmente en el equipo {equipoActual}.");
                Console.WriteLine("Ingrese el nuevo equipo para el jugador: ");
                string nuevoEquipo = Console.ReadLine();

                jugdores[jugador] = nuevoEquipo;
                Console.WriteLine($"El jugador {jugador} ha sido transferido al equipo {nuevoEquipo}.");
            }
            else
                Console.WriteLine("Este jugador no existe.");

            Console.WriteLine();
        }

        /// <summary>
        /// Damos de baja un jugador.
        /// 
        /// Comprobamos si existe con la clave / valor
        /// Si existe, se elimina con el método Remove
        /// </summary>
        private static void DarBajaJugador()
        {
            Console.Write("Ingrese el nombre del jugador a dar de baja: ");
            string jugador = Console.ReadLine();

            if (jugdores.Remove(jugador))
                Console.WriteLine($"El jugador {jugador} ha sido eliminado.");
            else
                Console.WriteLine("El equipo no existe.");

            Console.WriteLine();
            GuardarJugadores();
        }

        /// <summary>
        /// Damos de alta un jugdor
        /// 
        /// Comprobamos si existe con la clave / valor
        /// Si existe, añadimos nuevo jugador.
        /// </summary>
        private static void DarAltaJug()
        {
            Console.Write("Ingrese el nombre del jugador: ");
            string jugador = Console.ReadLine();

            if (jugdores.ContainsKey(jugador))
                Console.WriteLine("El jugador ya existe.");
            else
            {
                Console.Write("Ingrese el nombre del equipo: ");
                string equipo = Console.ReadLine();
                jugdores.Add(jugador, equipo);
                Console.WriteLine($"El jugador {jugador} ha sido dado de alta en el equipo {equipo}.");
            }

            Console.WriteLine();
            GuardarJugadores();
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
                Console.WriteLine("El equipo ya existe.");
            else
            {
                Console.Write("Ingrese la puntuación inicial: ");
                if (int.TryParse(Console.ReadLine(), out int puntuacion))
                {
                    equiposFutbol.Add(equipo, puntuacion);
                    Console.WriteLine($"El equipo {equipo} ha sido dado de alta con {puntuacion} puntos.");
                }
                else
                    Console.WriteLine("Puntuación inválida.");
            }

            Console.WriteLine();
            GuardarDatos();
        }

        /// <summary>
        /// Damos de baja un equipo de la clasificación.
        /// 
        /// Comprobamos si existe con la clave / valor
        /// Si existe, se elimina con el método Remove
        /// </summary>
        static void DarDeBaja()
        {
            Console.Write("Ingrese el nombre del equipo a eliminar: ");
            string equipo = Console.ReadLine();

            if (equiposFutbol.Remove(equipo))
                Console.WriteLine($"El equipo {equipo} ha sido eliminado.");
            else
                Console.WriteLine("El equipo no existe.");

            Console.WriteLine();
            GuardarDatos();
        }

        /// <summary>
        /// Modificamos la puntuación de un equipo
        /// 
        /// Comprobamos si existe con la clave / valor
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
                    Console.WriteLine("Puntuación inválida.");
            }
            else
                Console.WriteLine("El equipo no existe.");

            Console.WriteLine();
            GuardarDatos();
        }

        /// <summary>
        /// Mostramos la clasificación de los equipos
        /// </summary>
        static void MostrarClasificacion()
        {
            Console.WriteLine("Clasificación:");

            foreach (KeyValuePair<string, int> equipo in equiposFutbol.OrderByDescending(e => e.Value))
                Console.WriteLine($"{equipo.Key}: {equipo.Value} puntos");
        }

        /// <summary>
        /// Leer los datos de los equipos del fichero
        /// </summary>
        public static void LeerDatosEquipos()
        {
            using (StreamReader reader = new StreamReader(Path.Combine(Ruta, FicheroEquipos)))
            {
                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(';');

                    if (datos.Length == 2)
                    {
                        string equipo = datos[0];

                        if (int.TryParse(datos[1], out int puntuacion))
                            Console.WriteLine("Equipo: " + equipo + " Puntuación: " + puntuacion);
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Leer los datos de los jugadores de los ficheros
        /// </summary>
        public static void LeerDatosJugadores()
        {
            using (StreamReader reader = new StreamReader(Path.Combine(Ruta, FicheroJugadores)))
            {
                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(';');

                    if (datos.Length == 2)
                    {
                        string jugador = datos[0];
                        string equipo = datos[1];
                        Console.WriteLine("Jugador: " + jugador + " Equipo: " + equipo);
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Guardamos los datos de los equipos en un archivo de texto
        /// </summary>
        public static void GuardarDatos()
        {
            // Comprobar si la carpeta 'ficheros' existe; si no, la creamos
            if (!Directory.Exists(Ruta))
                Directory.CreateDirectory(Ruta);

            // Guardar los datos en el fichero de equipos
            using (StreamWriter writer = new StreamWriter(Path.Combine(Ruta, FicheroEquipos)))
            {
                foreach (KeyValuePair<string, int> equipo in equiposFutbol)
                    writer.WriteLine($"{equipo.Key};{equipo.Value}");
            }
        }


        /// <summary>
        /// Guardamos los datos de los jugadores en un archivo de texto
        /// </summary>
        /// <summary>
        /// Guardamos los datos de los jugadores en un archivo de texto
        /// </summary>
        static void GuardarJugadores()
        {
            // Comprobar si la carpeta 'ficheros' existe; si no, la creamos
            if (!Directory.Exists(Ruta))
                Directory.CreateDirectory(Ruta);

            // Guardar los datos en el fichero de jugadores
            using (StreamWriter writer = new StreamWriter(Path.Combine(Ruta, FicheroJugadores)))
            {
                foreach (KeyValuePair<string, string> jugador in jugdores)
                    writer.WriteLine($"{jugador.Key};{jugador.Value}");
            }
        }


    }
}