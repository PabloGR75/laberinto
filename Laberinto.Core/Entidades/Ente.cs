using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    /// Clase base para todos los entes del laberinto (Bicho, Personaje, etc).
    public abstract class Ente
    {
        private int _vidas;
        private int _poder;
        private Habitacion _posicion;
        private JuegoLaberinto _juego;
        private EstadoEnte _estadoEnte;

        // --- PROPIEDADES ---
        public int Vidas
        {
            get => _vidas;
            set
            {
                _vidas = value;
                // Notificación de cambio si usas INotifyPropertyChanged
            }
        }

        public int Poder
        {
            get => _poder;
            set => _poder = value;
        }

        public Habitacion Posicion
        {
            get => _posicion;
            set
            {
                _posicion = value;
                // Notificación de cambio si es necesario
            }
        }

        public JuegoLaberinto Juego
        {
            get => _juego;
            set => _juego = value;
        }

        public EstadoEnte EstadoEnte
        {
            get => _estadoEnte;
            set => _estadoEnte = value;
        }

        // --- CONSTRUCTOR ---
        protected Ente(JuegoLaberinto juego = null)
        {
            Inicialize(juego);
        }

        protected void Inicialize(JuegoLaberinto juego = null)
        {
            Vidas = 5;
            Poder = 1;
            EstadoEnte = new Vivo();
            Juego = juego;
        }

        // --- ATAQUES Y VIDA ---
        public virtual void Atacar()
        {
            EstadoEnte.Atacar(this);
        }

        public virtual void EsAtacadoPor(Ente alguien)
        {
            // Print de diagnóstico:
            // Console.WriteLine($"{this} es atacado por {alguien}");
            Vidas -= alguien.Poder;
            // Console.WriteLine($"Vidas: {Vidas}");
            if (Vidas <= 0)
            {
                HeMuerto();
            }
        }
        public virtual LaberintoObj JuegoClonarLaberinto()
        {
            return Juego?.ClonarLaberinto();
        }

        public virtual void HeMuerto()
        {
            EstadoEnte = new Muerto();
            Avisar();
        }

        public virtual bool EstaVivo() => Vidas > 0;

        // --- SUBCLASS RESPONSIBILITY ---
        public abstract void Avisar(string mensaje = null);

        public virtual void PuedeActuar()
        {
            // Por defecto, vacío. Sobrescribir en Bicho si corresponde.
        }

        // Acción
        public virtual void PuedeAtacar()
        {
            // Por defecto, vacío o lógica base
        }

        // Consulta (devuelve bool, ya la tienes si la necesitas en otras partes)
        public virtual bool PuedeAtacarA(Ente objetivo)
        {
            return false; // Por defecto, o tu lógica base
        }

        // --- MÉTODOS DE TÚNEL Y LABERINTO ---
        public virtual Tunel BuscarTunel()
        {
            // Por defecto, no hace nada, sobrescribir en subclases si corresponde
            return null;
        }

        public virtual void Actua() { }

        public virtual LaberintoObj CrearNuevoLaberinto(Tunel tunel)
        {
            // Implementa si necesitas crear un nuevo laberinto a partir de un túnel
            return null;
        }

        public virtual LaberintoObj JuegoClonaLaberinto()
        {
            return Juego?.ClonarLaberinto();
        }

        public virtual void EntrarEn(Habitacion unaHabitacion)
        {
            Posicion = unaHabitacion;
            // Aquí cualquier lógica adicional si la necesitas
        }
        
        // --- SETTERS Y GETTERS (Smalltalk style, solo si los necesitas explícitos) ---
        public int GetVidas() => Vidas;
        public void SetVidas(int v) => Vidas = v;
        public int GetPoder() => Poder;
        public void SetPoder(int p) => Poder = p;
        public Habitacion GetPosicion() => Posicion;
        public void SetPosicion(Habitacion h) => Posicion = h;
        public JuegoLaberinto GetJuego() => Juego;
        public void SetJuego(JuegoLaberinto j) => Juego = j;
        public EstadoEnte GetEstadoEnte() => EstadoEnte;
        public void SetEstadoEnte(EstadoEnte e) => EstadoEnte = e;
    }
}
