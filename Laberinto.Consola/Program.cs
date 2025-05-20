using Laberinto.Core;
using Laberinto.Core.Models;
using Laberinto.Core.Services;
using Laberinto.Core.Entidades;
using System;

namespace Laberinto.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
        // Crear el builder del laberinto
        var builder = new LaberintoBuilder();
        builder.InicializarLaberinto();
        builder.ConstruirHabitacion(1);
        builder.ConstruirHabitacion(2);
        builder.ConstruirPuerta(1, 2);
        var laberinto = builder.ObtenerLaberinto();

        // Crear un juego
        var juego = new JuegoLaberinto(laberinto);

        // Crear un bicho y ubicarlo en la habitación 1
        var bicho = new Bicho(juego);
        var hab1 = laberinto.ObtenerHabitacion(1);
        bicho.EntrarEn(hab1);

        Console.WriteLine("Bicho entra en la habitación: " + bicho.Posicion.Num);

        // Mover el bicho a la habitación vecina (por la puerta)
        var habVecina = hab1.ObtenerVecina(new Este());
        if (habVecina != null)
        {
            bicho.EntrarEn(habVecina);
            Console.WriteLine("Bicho se mueve a la habitación: " + bicho.Posicion.Num);
        }
        else
        {
            Console.WriteLine("No hay habitación vecina en esa dirección.");
        }

        // Prueba Visitor: recorrer e imprimir tipo de cada elemento
        var visitor = new DescripcionVisitor();
        laberinto.Recorrer(e => e.Accept(visitor));

        }
    }
}

