using System;

namespace Laberinto.Core.Models
{
    public class Pared : ElementoMapa
    {
        // Es un EM que no se puede atravesar

        public override void Entrar(Entidades.Ente alguien)
        {
            // Simula el mensaje de Smalltalk: ha chocado con una pared
            Console.WriteLine($"{alguien} ha chocado con una pared.");
        }
        public override ElementoMapa DeepClone()
        {
            var clone = (ElementoMapa)this.MemberwiseClone();

            // Clona la lista de comandos (nueva lista, mismos comandos - cambiar a deep si lo necesitas)
            clone.ComandosLista.Clear();
            foreach (var comando in this.ObtenerComandos())
            {
                clone.ComandosLista.Add(comando); // Si necesitas deep clone, comando.DeepClone()
            }

            // Clona otras propiedades si tenemos (como padre, etc.)
            // clone.Padre = ...;

            return clone;
        }

        public override bool EsPared => true;

        public override string ToString()
        {
            return "Pared";
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitPared(this);
        }
    }
}
