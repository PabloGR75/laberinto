// Bicho.cs
using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    /// Representa un bicho (ente autónomo que puede moverse).

    public class Bicho : Ente
    {
        public Modo Modo { get; protected set; }

        public Bicho(JuegoLaberinto? juego = null) : base(juego)
        {
            Modo = null; // O un modo por defecto, si tienes alguno.
        }

        /// Mueve el bicho en la orientación indicada.
        public virtual void Mover(Orientacion unaOrientacion)
        {
            var nuevaHabitacion = ObtenerNuevaHabitacion(unaOrientacion);
            if (nuevaHabitacion != null)
                EntrarEn(nuevaHabitacion);
        }

        /// Devuelve la nueva habitación según la orientación (si existe vecina).
        public virtual Habitacion ObtenerNuevaHabitacion(Orientacion unaOrientacion)
        {
            return Posicion?.ObtenerVecina(unaOrientacion);
        }
    }
}

