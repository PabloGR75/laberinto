using Laberinto.Core.Models;

namespace Laberinto.Core.Visitor
{
    public class VisitorActivarBombas : IVisitor
    {
        public void VisitBomba(Bomba bomba)
        {
            bomba.Activar();
        }

        // Métodos vacíos para los demás tipos (puedes añadir lógica si fuera necesario)
        public void VisitHabitacion(Habitacion habitacion) { }
        public void VisitLaberinto(LaberintoObj laberinto) { }
        public void VisitArmario(Armario armario) { }
        public void VisitPuerta(Puerta puerta) { }
        public void VisitTunel(Tunel tunel) { }
        public void VisitPared(Pared pared) { }
        public void VisitParedBomba(ParedBomba paredBomba) { }
        public void VisitHoja(Hoja hoja) { }
        public void VisitContenedor(Contenedor contenedor) { }
    }
}
