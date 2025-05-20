// JuegoLaberinto.cs
using System.Collections.Generic;
using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core
{
    /// <summary>
    /// Controlador principal del juego del laberinto.
    /// Gestiona entes, turnos, estado global y lógica de partida.
    /// </summary>
    public class JuegoLaberinto
    {
        /// <summary>
        /// El laberinto sobre el que se juega.
        /// </summary>
        public LaberintoObj Laberinto { get; private set; }

        /// <summary>
        /// Entes activos en la partida (jugadores, bichos…).
        /// </summary>
        public List<Ente> Entes { get; } = new();

        /// <summary>
        /// Turno actual.
        /// </summary>
        public int Turno { get; private set; } = 1;

        /// <summary>
        /// Estado del juego (puedes ampliar este enum según tu lógica).
        /// </summary>
        public EstadoJuego Estado { get; private set; } = EstadoJuego.EnCurso;

        public JuegoLaberinto(LaberintoObj laberinto)
        {
            Laberinto = laberinto;
        }

        /// <summary>
        /// Añade un ente (jugador, bicho, etc.) a la partida.
        /// </summary>
        public void AñadirEnte(Ente ente)
        {
            Entes.Add(ente);
        }

        /// <summary>
        /// Avanza al siguiente turno.
        /// </summary>
        public void SiguienteTurno()
        {
            Turno++;
            // Aquí puedes meter lógica de activación de bichos, eventos, etc.
        }

        /// <summary>
        /// Finaliza la partida.
        /// </summary>
        public void Finalizar()
        {
            Estado = EstadoJuego.Finalizado;
        }

        /// <summary>
        /// Reinicia la partida.
        /// </summary>
        public void Reiniciar()
        {
            Turno = 1;
            Estado = EstadoJuego.EnCurso;
            // También puedes limpiar entes, resetear posiciones, etc.
        }

        public LaberintoObj ClonarLaberinto()
        {
            // Aquí deberías hacer una copia profunda ("deep copy") del laberinto.
            // Por ahora, devuelvo el mismo (ajusta según tu modelo)
            return Laberinto; // O usa un método de clonación real.
        }

        public LaberintoObj CrearNuevoLaberinto()
        {
            // Aquí puedes crear un nuevo laberinto vacío o a partir de alguna plantilla.
            Laberinto = new LaberintoObj();
            return Laberinto;
        }
    }

    /// Estados posibles del juego.
    public enum EstadoJuego
    {
        EnCurso,
        Finalizado
        // Añade otros (Pausado, Ganado, Perdido...) según tu lógica.
    }
}
