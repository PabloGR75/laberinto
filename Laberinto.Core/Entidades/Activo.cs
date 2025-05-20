// Activo.cs
using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    /// <summary>
    /// Un bicho que siempre se mueve cuando se lo ordenan.
    /// </summary>
    public class Activo : Bicho
    {
        public Activo(JuegoLaberinto? juego = null) : base(juego) { }

        public override void Mover(Orientacion unaOrientacion)
        {
            base.Mover(unaOrientacion);
        }
    }
}
