// DescripcionVisitor.cs
using System;
namespace Laberinto.Core.Models
{
    public class DescripcionVisitor : IVisitor
    {
        public void VisitHabitacion(Habitacion habitacion)
        {
            Console.WriteLine($"Habitación {habitacion.Num}");
        }

        public void VisitArmario(Armario armario)
        {
            Console.WriteLine("Armario");
        }

        public void VisitLaberinto(LaberintoObj laberinto)
        {
            Console.WriteLine("Laberinto");
        }

        public void VisitPuerta(Puerta puerta)
        {
            Console.WriteLine(puerta.ToString());
        }

        public void VisitTunel(Tunel tunel)
        {
            Console.WriteLine("Túnel");
        }

        public void VisitPared(Pared pared)
        {
            Console.WriteLine("Pared");
        }

        public void VisitParedBomba(ParedBomba paredBomba)
        {
            Console.WriteLine("Pared Bomba");
        }

        public void VisitBomba(Bomba bomba)
        {
            Console.WriteLine("¡Bomba!");
        }

        public void VisitHoja(Hoja hoja)
        {
            Console.WriteLine("Hoja");
        }

        public void VisitContenedor(Contenedor contenedor)
        {
            Console.WriteLine("Contenedor");
        }

        public void VisitPocima(Pocima pocima)
        {
            Console.WriteLine("Pócima");
        }
        public void VisitLampara(Lampara lampara)
        {
            Console.WriteLine("Lámpara");
        }

        public void VisitCuadro(Cuadro cuadro)
        {
            Console.WriteLine("Cuadro");
        }

    }
}
