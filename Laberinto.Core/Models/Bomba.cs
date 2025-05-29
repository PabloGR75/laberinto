using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Bomba : Decorator
    {
        public bool Activa { get; set; }

        public Bomba() : base()
        {
            Activa = true;
        }

        public Bomba(ElementoMapa em) : base(em)
        {
            Activa = false;
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

        public void Activar()
        {
            // Mensaje de depuración: bomba activa
            Console.WriteLine("Bomba activa");
            Activa = true;
        }

        public void Explotar(Personaje personaje)
        {
            if (!Activa) return;
            personaje.RecibirDanno(1); // o personaje.Vidas--
            Activa = false;
        }

        public override void Entrar(Ente alguien)
        {
            if (Activa)
            {
                //Console.WriteLine($"{alguien} ha chocado con una bomba.");
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
