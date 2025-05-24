using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Armario : Contenedor
    {
        public int Num { get; private set; }
        public Habitacion HabitacionMadre { get; private set; }

        // Constructor con número y habitación
        public Armario(int num, Habitacion habitacion)
        {
            Num = num;
            HabitacionMadre = habitacion;
            habitacion.AgregarHijo(this); // Te asegura la integración
        }

        public override bool EsArmario => true;

        public override void VisitarContenedor(IVisitor visitor)
        {
            visitor.VisitArmario(this);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitArmario(this);
        }

        public override string ToString()
        {
            return $"Armario{Num}";
        }
    }
}

