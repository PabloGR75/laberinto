using System.Text.Json;

using Laberinto.Core;
using Laberinto.Core.Models;
using Laberinto.Core.Services;
using Laberinto.Core.Entidades;

namespace Laberinto.Consola
{
    class Historico
    {
        static void Main(string[] args)
        {
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

            // Crear personaje
            Console.Write("Introduce el nombre del personaje: ");
            var nombre = Console.ReadLine();
            juego.AgregarPersonaje(nombre);

            // Menú/bucle de juego
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\nComandos: mover norte/sur/este/oeste, atacar, abrir puertas, cerrar puertas, estado, salir");
                Console.Write("Acción: ");
                var comando = Console.ReadLine()?.Trim().ToLower();

                switch (comando)
                {
                    case "mover norte":
                        juego.Person.IrAlNorte();
                        break;
                    case "mover sur":
                        juego.Person.IrAlSur();
                        break;
                    case "mover este":
                        juego.Person.IrAlEste();
                        break;
                    case "mover oeste":
                        juego.Person.IrAlOeste();
                        break;
                    case "atacar":
                        juego.Person.Atacar(); // Ajusta si el método se llama distinto
                        break;
                    case "abrir puertas":
                        juego.AbrirPuertas();
                        Console.WriteLine("¡Todas las puertas abiertas!");
                        break;
                    case "cerrar puertas":
                        juego.CerrarPuertas();
                        Console.WriteLine("¡Todas las puertas cerradas!");
                        break;
                    case "estado":
                        MostrarEstado(juego);
                        break;
                    case "salir":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Comando no reconocido.");
                        break;
                }

                // Comprobación de victoria/derrota (ajusta a tu lógica)
                if (!juego.Person.EstaVivo())
                {
                    Console.WriteLine("¡Has muerto! Game Over.");
                    salir = true;
                }
                else if (juego.TodosLosBichosMuertos()) // Si tienes este método
                {
                    Console.WriteLine("¡Has ganado! Todos los bichos han sido derrotados.");
                    salir = true;
                }
            }

            Console.WriteLine("Fin del juego.");
        }


        static void MostrarEstado(JuegoLaberinto juego)
        {
            var person = juego.Person;
            Console.WriteLine($"Posición: Habitación {person.Posicion.Num} - Vidas: {person.Vidas}");
            // Añade aquí cualquier info adicional (bichos, puertas, objetos, etc.)
        }

        //----------------------------------------------------------------------------------------

        //// Ruta al archivo JSON (ajusta la ruta si necesario)
        // var rutaJson = Path.Combine("Laberintos", "lab4Hab.json");
        // string json = File.ReadAllText(rutaJson);

        // // Deserializar a Dictionary<string, object>
        // var options = new JsonSerializerOptions
        // {
        //     PropertyNameCaseInsensitive = true
        // };

        // var dict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, options);
        // var dictObj = JsonHelper.ToObjectDictionary(dict);

        // // Crear el Director (el orquestador que lee el JSON y construye el juego)
        // var director = new Director();

        // // Procesar el archivo JSON para construir el juego
        // director.Procesar(dictObj);

        // // Obtener el juego creado
        // var juego = director.ObtenerJuego() as JuegoLaberinto;
        // if (juego == null)
        // {
        //     Console.WriteLine("Error: el objeto no es un JuegoLaberinto.");
        //     return;
        // }

        // // Agregar un personaje al juego (por ejemplo, "Pepe")
        // juego.AgregarPersonaje("Pepe");

        // // Puedes obtener el personaje para realizar acciones
        // var person = juego.Person;

        // // Opcional: Mostrar estado inicial por consola
        // Console.WriteLine("¡Laberinto cargado y personaje creado!");
        // Console.WriteLine($"Posición inicial del personaje: Habitación {person.Posicion?.Num}");

        // Console.WriteLine("----- Recorrido del laberinto -----");

        // // Recorre el laberinto y muestra información de cada elemento
        // juego.Laberinto.Recorrer(em =>
        // {
        //     switch (em)
        //     {
        //         case Habitacion hab:
        //             Console.WriteLine($"Habitación {hab.Num}");
        //             // Si quieres, también puedes mostrar sus hijos:
        //             foreach (var hijo in hab.Hijos)
        //             {
        //                 Console.WriteLine($"  - Hijo: {hijo.GetType().Name}");
        //             }
        //             break;

        //         case Armario arm:
        //             if (arm.Padre is Habitacion habPadre)
        //                 Console.WriteLine($"  Armario en habitación {habPadre.Num}");
        //             else if (arm.Padre is Contenedor cont)
        //                 Console.WriteLine($"  Armario en contenedor número {cont.Num}");
        //             else
        //                 Console.WriteLine("  Armario sin habitación padre");
        //             break;

        //         case Puerta puerta:
        //             Console.WriteLine($"Puerta entre {puerta.Lado1?.Num} y {puerta.Lado2?.Num} (Abierta: {puerta.EstaAbierta()})");
        //             break;
        //     }

        //     foreach (var bicho in juego.Bichos)
        //     {
        //         Console.WriteLine($"Bicho en habitación {bicho.Posicion?.Num} (Modo: {bicho.Modo?.GetType().Name})");
        //     }
        // });

        // Console.WriteLine("----- Fin del recorrido -----");

        // // Ejemplo de mover al personaje
        // person.IrAlNorte();
        // person.IrAlSur();
        // person.IrAlEste();
        // person.IrAlOeste();

        // // Puedes atacar, abrir puertas, lanzar bichos, etc.
        // person.Atacar();
        // juego.AbrirPuertas();
        // juego.CerrarPuertas();
        // juego.LanzarBichos();
        // juego.TerminarBichos();

        // Console.WriteLine("Fin de la partida de prueba.");

        //----------------------------------------------------------------------------------------

        // // Crear el builder del laberinto
        // var builder = new LaberintoBuilder();
        // builder.InicializarLaberinto();
        // builder.FabricarHabitacion(1);
        // builder.FabricarHabitacion(2);
        // builder.FabricarPuerta(1, 2);
        // var laberinto = builder.ObtenerLaberinto();

        // // Crear un juego
        // var juego = new JuegoLaberinto(laberinto);

        // // Crear un bicho y ubicarlo en la habitación 1
        // var bicho = new Bicho(new Perezoso());
        // var hab1 = laberinto.ObtenerHabitacion(1);
        // bicho.EntrarEn(hab1);

        // Console.WriteLine("Bicho entra en la habitación: " + bicho.Posicion.Num);

        // // Mover el bicho a la habitación vecina (por la puerta)
        // var habVecina = hab1.ObtenerVecina(Este.Instancia);
        // if (habVecina != null)
        // {
        //     bicho.EntrarEn(habVecina);
        //     Console.WriteLine("Bicho se mueve a la habitación: " + bicho.Posicion.Num);
        // }
        // else
        // {
        //     Console.WriteLine("No hay habitación vecina en esa dirección.");
        // }

        // // Prueba Visitor: recorrer e imprimir tipo de cada elemento
        // var visitor = new DescripcionVisitor();
        // laberinto.Recorrer(e => e.Accept(visitor));

    }
}


