using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    public class Bicho : Ente
    {
        // El "modo" define el comportamiento del bicho (Agresivo, Perezoso)
        public Modo Modo { get; set; }

        // Poder se usa en ataques y lo gestionan los métodos iniX y los factories
        public int Poder
        {
            get => base.Poder;
            set => base.Poder = value;
        }

        // Constructor principal: establece el modo y los valores por defecto
        public Bicho(Modo modo = null, JuegoLaberinto juego = null)
            : base(juego)
        {
            Modo = modo ?? new Perezoso();
            Poder = Modo is Agresivo ? 10 : 1;
        }

        // Acceso directo a la orientación actual de la habitación
        public Orientacion ObtenerOrientacion()
        {
            return Posicion?.ObtenerOrientacion();
        }

        public void RecibirDanno(int danno)
        {
            this.Vidas -= danno;
            if (this.Vidas <= 0)
            {
                this.Vidas = 0;
                this.HeMuerto();
            }
            //Console.WriteLine($"[DEBUG] Bicho {this.Modo?.GetType().Name} recibe {danno} de daño. Vidas restantes: {this.Vidas}");
        }

        // Comprobaciones rápidas de tipo de modo
        public bool EsAgresivo() => Modo is Agresivo;
        public bool EsPerezoso() => Modo is Perezoso;

        // Inicializaciones rápidas de modo (usado en los factories o cuando cambias el modo)
        public void IniAgresivo()
        {
            Modo = new Agresivo();
            Poder = 10;
        }
        public void IniPerezoso()
        {
            Modo = new Perezoso();
            Poder = 1;
        }

        // Actuar (turno del bicho)
        public override void Actua()
        {
            EstadoEnte.Actua(this);
        }

        public void MoverAleatoriamente()
        {
            if (this.Posicion is Habitacion hab && hab.Puertas.Count > 0)
            {
                var random = new Random();
                var orientacion = hab.Puertas.Keys.ElementAt(random.Next(hab.Puertas.Count));
                var puerta = hab.Puertas[orientacion];

                if (puerta.EstaAbierta())
                {
                    var otraHabitacion = puerta.OtroLado(hab) as Habitacion;
                    if (otraHabitacion != null)
                    {
                        this.Posicion = otraHabitacion;
                    }
                }
            }
        }

        // Puede actuar (equivalente en Smalltalk)
        public void PuedeActuar()
        {
            Modo.Actua(this);
        }

        // Buscar túnel (delegado al modo)
        public override Tunel BuscarTunel()
        {
            return Modo.BuscarTunelBicho(this);
        }

        // ¿Puede atacar? (ejemplo: busca personaje, puedes adaptar la lógica)
        public override void PuedeAtacar()
        {
            if (Juego?.Person?.EstaEscondido ?? false) return;
            Juego?.BuscarPersonaje(this);
        }

        public override bool PuedeAtacarA(Ente objetivo)
        {
            if (objetivo is Personaje personaje && personaje.EstaEscondido)
                return false; // No atacar si el personaje está escondido
            // Devuelve si puede atacar a ese ente
            return this.Juego != null &&
                this.Posicion == objetivo.Posicion &&
                this.EstaVivo() &&
                objetivo.EstaVivo();
        }

        // Avisar (termina el bicho en el juego)
        public override void Avisar(string mensaje)
        {
            Juego?.TerminarBicho(this);
        }

        // Para impresión tipo printOn:
        public override string ToString()
        {
            return $"Bicho-{Modo}";
        }
    }
}
