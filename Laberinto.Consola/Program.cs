using System.Text.Json;

using Laberinto.Core;
using Laberinto.Core.Models;
using Laberinto.Core.Services;
using Laberinto.Core.Entidades;

namespace Laberinto.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            string rutaJson = "Laberintos/lab4Hab4Bichos.json"; // Pon la ruta relativa correcta
            if (!File.Exists(rutaJson))
            {
                Console.WriteLine("No se encuentra el archivo del laberinto.");
                return;
            }

            // Cargar JSON
            var json = File.ReadAllText(rutaJson);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);
            var director = new Director();

            // Procesar y obtener el juego
            director.Procesar(dict);
            var juego = director.ObtenerJuego() as JuegoLaberinto;

            var habitaciones = juego.Laberinto.Hijos.OfType<Habitacion>().ToList();
            //Console.WriteLine("Habitaciones en el laberinto:");
            foreach (var hab in habitaciones)
            {
                //Console.WriteLine($"- Habitación {hab.Num}");
            }

            // Crear personaje
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Introduce el nombre del personaje: ");
            var nombre = Console.ReadLine();
            Console.Clear();
            juego.AgregarPersonaje(nombre);

            if (juego.Person == null)
            {
                Console.WriteLine("Error: No se ha creado el personaje.");
                return;
            }
            if (juego.Person.Posicion == null)
            {
                Console.WriteLine("Error: El personaje no tiene posición inicial.");
                return;
            }
            else
            {
                //Console.WriteLine($"El personaje {juego.Person.Nombre} está en Habitación {juego.Person.Posicion.Num}.");
            }

            bool juegoActivo = true;

            while (juegoActivo)
            {
                //Console.Clear();
                // Mostrar estado actual del personaje y bichos
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine($"Personaje: {juego.Person.Nombre} - Vidas: {juego.Person.Vidas} - Posición: Habitación {juego.Person.Posicion.Num}");
                Console.WriteLine("Bichos vivos: " + juego.Bichos.Count(b => b.EstaVivo()));
                Console.WriteLine("----------------------------------------------------------");
                //Console.WriteLine($"[DEBUG] Habitación actual tiene {juego.Person.Posicion.Puertas.Count} puertas.");

                // Mostrar acciones disponibles
                Console.WriteLine("Acciones que consumen turno:");
                Console.WriteLine("1. Ir al Norte");
                Console.WriteLine("2. Ir al Sur");
                Console.WriteLine("3. Ir al Este");
                Console.WriteLine("4. Ir al Oeste");
                Console.WriteLine("5. Atacar");
                Console.WriteLine("6. Inspeccionar habitación");
                Console.WriteLine("7. Abrir todas las puertas");
                Console.WriteLine("8. Cerrar todas las puertas");
                Console.WriteLine("9. Ver puertas de la habitación actual");
                Console.WriteLine("0. Salir");

                Console.Write("\nElige acción: ");
                var opcion = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("----------------------------------------------------------");

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine(juego.Person.MoverA(Norte.Instancia));
                        juego.EjecutarTurnoBichos();
                        break;
                    case "2":
                        Console.WriteLine(juego.Person.MoverA(Sur.Instancia));
                        juego.EjecutarTurnoBichos();
                        break;
                    case "3":
                        Console.WriteLine(juego.Person.MoverA(Este.Instancia));
                        juego.EjecutarTurnoBichos();
                        break;
                    case "4":
                        Console.WriteLine(juego.Person.MoverA(Oeste.Instancia));
                        juego.EjecutarTurnoBichos();
                        break;
                    case "5":
                        Console.WriteLine(juego.Person.AtacarEnHabitacionActual(juego));
                        juego.EjecutarTurnoBichos();
                        break;
                    case "6": // Inspeccionar habitación
                        var habitacionActual = juego.Person.Posicion as Habitacion;
                        if (habitacionActual != null)
                        {
                            Console.WriteLine(habitacionActual.Describir(juego));
                        }
                        break;
                    case "7":
                        juego.AbrirTodasLasPuertas();
                        //Console.WriteLine("Estado de las puertas tras abrirlas:");
                        foreach (var puerta in juego.Laberinto.ObtenerTodasLasPuertas())
                        {
                            var l1 = puerta.Lado1 as Habitacion;
                            var l2 = puerta.Lado2 as Habitacion;
                            //Console.WriteLine($"Puerta conecta {l1?.Num} y {l2?.Num}: {(puerta.EstaAbierta() ? "Abierta" : "Cerrada")}");
                        }
                        break;
                    case "8":
                        juego.CerrarTodasLasPuertas();
                        //Console.WriteLine("Estado de las puertas tras cerrarlas:");
                        foreach (var puerta in juego.Laberinto.ObtenerTodasLasPuertas())
                        {
                            var l1 = puerta.Lado1 as Habitacion;
                            var l2 = puerta.Lado2 as Habitacion;
                            //Console.WriteLine($"Puerta conecta {l1?.Num} y {l2?.Num}: {(puerta.EstaAbierta() ? "Abierta" : "Cerrada")}");
                        }
                        break;
                    case "9":
                        var habActual = juego.Person.Posicion as Habitacion;
                        if (habActual != null)
                        {
                            // Mostrar Puertas
                            foreach (var kvp in habActual.Puertas)
                            {
                                var orientacion = kvp.Key; // Esto es una instancia de Orientacion
                                var puerta = kvp.Value;
                                var l1 = puerta.Lado1 as Habitacion;
                                var l2 = puerta.Lado2 as Habitacion;
                                if (l1 == null || l2 == null || l1.Num == 0 || l2.Num == 0)
                                {
                                    Console.WriteLine("[DEBUG] Puerta con referencia nula o habitación sin número. Se omite.");
                                    continue;
                                }
                                Console.WriteLine($"- Puerta dirección {orientacion}: Conecta habitación {(l1?.Num ?? -1)} y {(l2?.Num ?? -1)} - {(puerta.EstaAbierta() ? "Abierta" : "Cerrada")}");
                            }
                        }
                        break;

                    case "0":
                        juegoActivo = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                // Comprobar condiciones de fin de partida
                if (!juego.Person.EstaVivo())
                {
                    Console.WriteLine("¡Has perdido! El personaje ha muerto.");
                    juegoActivo = false;
                }
                else if (juego.TodosLosBichosMuertos())
                {
                    Console.WriteLine("¡Enhorabuena! Has eliminado a todos los bichos.");
                    juegoActivo = false;
                }
            }
            Console.WriteLine("Fin del juego.");
        }

    }
}


