// IVisitor.cs
namespace Laberinto.Core.Models
{
    public interface IVisitor
    {
        void VisitHabitacion(Habitacion habitacion);
        void VisitLaberinto(LaberintoObj laberinto);
        void VisitArmario(Armario armario);
        void VisitBomba(Bomba bomba);
        void VisitPuerta(Puerta puerta);
        void VisitTunel(Tunel tunel);
        void VisitPared(Pared pared);
        void VisitTesoro(Tesoro tesoro);
        void VisitHoja(Hoja hoja);
        void VisitContenedor(Contenedor contenedor);
    }
}
