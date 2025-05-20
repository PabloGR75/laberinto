using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Tunel : Hoja
    {
        public Habitacion HabitacionA { get; }
        public Habitacion HabitacionB { get; }

        public Tunel(Habitacion a, Habitacion b)
        {
            HabitacionA = a;
            HabitacionB = b;
        }

        public Habitacion OtroLado(Habitacion desde)
        {
            if (desde == HabitacionA) return HabitacionB;
            if (desde == HabitacionB) return HabitacionA;
            return null;
        }

        public override bool EsTunel => true;

        public override void Entrar(Ente quien)
        {
            // No hace nada por defecto.
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitTunel(this);
        }
    }
}
