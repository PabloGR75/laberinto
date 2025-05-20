// Personaje.cs
using Laberinto.Core;

namespace Laberinto.Core.Entidades
{
    /// <summary>
    /// Representa el personaje controlado por el jugador.
    /// </summary>
    public class Personaje : Ente
    {
        public Personaje(JuegoLaberinto? juego = null) : base(juego)
        {
            // No necesitas inicializar Juego aquí, ya lo hace la clase base Ente.
        }

        // Si necesitas lógica especial, añádela aquí.
    }
}
