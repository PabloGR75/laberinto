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
