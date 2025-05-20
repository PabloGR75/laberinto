// Perezoso.cs
using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    /// <summary>
    /// Un bicho que solo se mueve la mitad de las veces.
    /// </summary>
    public class Perezoso : Bicho
    {
        private static readonly Random _random = new Random();

        public Perezoso(JuegoLaberinto? juego = null) : base(juego) { }

        public override void Mover(Orientacion unaOrientacion)
        {
            if (_random.Next(2) == 0)
                base.Mover(unaOrientacion);
            // Si no, no se mueve.
        }
    }
}
