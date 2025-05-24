using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Bomba : Decorator
    {
        public bool Activa { get; set; }

        public Bomba() : base()
        {
            Activa = false;
        }

        public Bomba(ElementoMapa em) : base(em)
        {
            Activa = false;
        }

        public void Activar()
        {
            // Mensaje de depuración: bomba activa
            Console.WriteLine("Bomba activa");
            Activa = true;
        }

        public override void Entrar(Ente alguien)
        {
            if (Activa)
            {
                Console.WriteLine($"{alguien} ha chocado con una bomba.");
                // Aquí puedes definir lógica de explosión o daño
            }
            else
            {
                EM?.Entrar(alguien);
            }
        }

        public override bool EsBomba => true;

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitBomba(this);
        }

        public override string ToString() => "Bomba";
    }
}
