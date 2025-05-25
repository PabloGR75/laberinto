using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Tunel : Hoja
    {
        public LaberintoObj Laberinto { get; set; }

        public Tunel()
        {
            Laberinto = null;
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

        // El personaje crea un nuevo laberinto al entrar si aún no existe
        public void CrearNuevoLaberinto(Ente ente)
        {
            Console.WriteLine($"{ente} crea un nuevo laberinto");
            Laberinto = ente.JuegoClonarLaberinto();
        }

        public override void Entrar(Ente alguien)
        {
            if (Laberinto == null)
            {
                CrearNuevoLaberinto(alguien);
            }
            else
            {
                Console.WriteLine($"{alguien} entra en un nuevo laberinto");
                Laberinto.Entrar(alguien);
            }
        }

        public override bool EsTunel => true;

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitTunel(this);
        }

        public override string ToString() => "Tunel";
    }
}
