using Laberinto.Core.Models;
using System;

namespace Laberinto.Core.Visitor
{
    public class VisitorInventario : IVisitor
    {
        public void VisitArmario(Armario armario)
        {
            Console.WriteLine(armario.ToString());
        }

        public void VisitBomba(Bomba bomba)
        {
            Console.WriteLine(bomba.ToString());
        }

        public void VisitHabitacion(Habitacion habitacion)
        {
            Console.WriteLine(habitacion.ToString());
        }

        public void VisitPared(Pared pared)
        {
            Console.WriteLine(pared.ToString());
        }

        public void VisitPuerta(Puerta puerta)
        {
            Console.WriteLine(puerta.ToString());
        }

        public void VisitTunel(Tunel tunel)
        {
            Console.WriteLine(tunel.ToString());
        }

        // Métodos vacíos para los demás tipos
        public void VisitLaberinto(LaberintoObj laberinto) { }
        public void VisitParedBomba(ParedBomba paredBomba) { }
        public void VisitHoja(Hoja hoja) { }
        public void VisitContenedor(Contenedor contenedor) { }
    }
}
