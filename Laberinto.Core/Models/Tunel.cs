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
