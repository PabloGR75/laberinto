using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Armario : Contenedor
    {
        public int Num { get; private set; }
        public Habitacion HabitacionMadre { get; private set; }
        public bool PersonajeEscondido { get; private set; }

        // Constructor con número y habitación
        public Armario(int num, Habitacion habitacion)
        {
            Num = num;
            HabitacionMadre = habitacion;
            habitacion.AgregarHijo(this); // Te asegura la integración
            PersonajeEscondido = false;
        }

        public override bool EsArmario => true;

        public override ElementoMapa DeepClone()
        {
            var clone = (Armario)base.DeepClone();
            // Propiedades extra si hay
            return clone;
        }

        // Método para que el personaje se esconda
        public void EsconderPersonaje(Personaje personaje)
        {
            if (!PersonajeEscondido)
            {
                PersonajeEscondido = true;
                personaje.Posicion = this.HabitacionMadre; // Mantener posición en la habitación
                Console.WriteLine($"{personaje.Nombre} se ha escondido en el armario {Num}.");
            }
        }

        public void SacarPersonaje(Personaje personaje)
        {
            if (PersonajeEscondido)
            {
                PersonajeEscondido = false;
                Console.WriteLine($"{personaje.Nombre} ha salido del armario {Num}.");
            }
        }

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

