// Ente.cs
using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    /// <summary>
    /// Representa un ente (jugador, bicho, personaje, etc.) en el laberinto.
    /// Traducción fiel del modelo Smalltalk.
    /// </summary>
    public class Ente
    {
        public int Poder { get; protected set; }
        public Habitacion Posicion { get; protected set; }
        public int Vidas { get; protected set; }
        public JuegoLaberinto Juego { get; protected set; }
        public EstadoEnte Estado { get; set; }

        public Ente(JuegoLaberinto? juego = null)
        {
            // Smalltalk: initialize
            Poder = 1;
            Posicion = null;
            Vidas = 1;
            Estado = new Vivo();
            Juego = juego;
        }

        // --- Métodos traducidos fielmente del Smalltalk ---

        /// <summary>
        /// El ente actúa en su turno (lógica polimórfica según estado).
        /// </summary>
        public virtual void Actua()
        {
            Estado.Actua(this);
        }

        /// <summary>
        /// Ataca a otro ente (lógica polimórfica según estado).
        /// </summary>
        public virtual void Atacar(Ente objetivo)
        {
            Estado.Atacar(this, objetivo);
        }

        /// <summary>
        /// Recibe un ataque de otro ente.
        /// </summary>
        public virtual void EsAtacadoPor(Ente otroEnte)
        {
            PerderVida();
        }

        /// <summary>
        /// Pierde una vida y comprueba si ha muerto.
        /// </summary>
        public virtual void PerderVida()
        {
            Vidas--;
            if (Vidas == 0)
            {
                HeMuerto();
            }
        }

        /// <summary>
        /// Cambia el estado a muerto.
        /// </summary>
        public virtual void HeMuerto()
        {
            Estado = new Muerto();
        }

        /// <summary>
        /// Indica si el ente está vivo.
        /// </summary>
        public virtual bool EstaVivo()
        {
            return Estado.EstaVivo;
        }

        /// <summary>
        /// Recibe un mensaje. Por defecto, no hace nada.
        /// </summary>
        public virtual void Avisar(string mensaje)
        {
            // Por defecto, vacío. Override en subclases si hace falta.
        }

        /// <summary>
        /// Busca un túnel desde la posición actual.
        /// </summary>
        public virtual Tunel BuscarTunel()
        {
            return Posicion?.BuscarTunel();
        }

        /// <summary>
        /// Clona el laberinto desde el juego.
        /// </summary>
        public virtual LaberintoObj JuegoClonarLaberinto()
        {
            return Juego?.ClonarLaberinto();
        }

        /// <summary>
        /// Indica si puede atacar a otro ente (ambos vivos).
        /// </summary>
        public virtual bool PuedeAtacar(Ente unEnte)
        {
            return EstaVivo() && unEnte.EstaVivo();
        }

        /// <summary>
        /// Crea un nuevo laberinto desde el juego.
        /// </summary>
        public virtual LaberintoObj CrearNuevoLaberinto()
        {
            return Juego?.CrearNuevoLaberinto();
        }

        /// <summary>
        /// Entra en una habitación.
        /// </summary>
        public virtual void EntrarEn(Habitacion unaHabitacion)
        {
            Posicion = unaHabitacion;
        }
    }
}
