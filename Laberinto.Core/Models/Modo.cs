// Modo.cs
using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    /// <summary>
    /// Representa una estrategia de movimiento para un bicho.
    /// </summary>
    public abstract class Modo
    {
        /// <summary>
        /// Lógica de movimiento según el modo.
        /// </summary>
        public abstract void Mover(Bicho bicho, Orientacion orientacion);
    }

    // Ejemplo de modo concreto: modo normal
    public class ModoNormal : Modo
    {
        public override void Mover(Bicho bicho, Orientacion orientacion)
        {
            bicho.Mover(orientacion);
        }
    }
    // Puedes implementar otros modos concretos según sea necesario.
}
