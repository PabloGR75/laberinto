// Puerta.cs
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Puerta : ElementoMapa
    {
        private Habitacion habitacionA;
        private Habitacion habitacionB;

        // El estado actual de la puerta (abierta o cerrada)
        public EstadoPuerta Estado { get; set; }

        public Puerta(Habitacion a, Habitacion b)
        {
            habitacionA = a;
            habitacionB = b;
            Estado = new Cerrada();
        }

        public void Abrir()
        {
            Estado.Abrir(this);
        }

        public void Cerrar()
        {
            Estado.Cerrar(this);
        }

        public bool EstaAbierta => Estado.EstaAbierta;

        public Habitacion OtroLado(Habitacion desde)
        {
            if (desde == habitacionA) return habitacionB;
            if (desde == habitacionB) return habitacionA;
            return null;
        }

        public override void Entrar(Ente quien)
        {
            // Lógica extra si quieres, por defecto nada.
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitPuerta(this);
        }
    }
}
