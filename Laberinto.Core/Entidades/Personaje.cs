using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    public class Personaje : Ente
    {
        public string Nombre { get; set; }

        // Constructor que recibe nombre y opcionalmente el juego
        public Personaje(string nombre, JuegoLaberinto? juego = null) : base(juego)
        {
            Nombre = nombre;
        }

        // Constructor alternativo sin nombre (por si lo necesitas)
        public Personaje(JuegoLaberinto? juego = null) : base(juego)
        {
            Nombre = string.Empty;
        }

        // Cuando el personaje muere, notifica al juego
        public override void Avisar(string mensaje = "")
        {
            Juego?.MuerePersonaje();
        }

        // Movimiento cardinal
        public void IrAlNorte()
        {
            Posicion?.IrAlNorte(this);
        }
        public void IrAlSur()
        {
            Posicion?.IrAlSur(this);
        }
        public void IrAlEste()
        {
            Posicion?.IrAlEste(this);
        }
        public void IrAlOeste()
        {
            Posicion?.IrAlOeste(this);
        }
        public void IrAlNoreste()
        {
            Posicion?.IrAlNoreste(this);
        }
        public void IrAlNoroeste()
        {
            Posicion?.IrAlNoroeste(this);
        }
        public void IrAlSureste()
        {
            Posicion?.IrAlSureste(this);
        }
        public void IrAlSuroeste()
        {
            Posicion?.IrAlSuroeste(this);
        }

        // Obtener comandos desde la posición actual (recorriendo el mapa)
        public List<Comando> ObtenerComandos()
        {
            var lista = new List<Comando>();
            Posicion?.Recorrer(each =>
            {
                if (each is ElementoMapa em)
                    lista.AddRange(em.ObtenerComandos());
            });
            return lista;
        }

        // Para delegar en el juego la acción de crear un nuevo laberinto
        public void CrearNuevoLaberinto(Tunel unTunel)
        {
            unTunel.CrearNuevoLaberinto(this);
        }

        // PuedeAtacar delega la búsqueda de bicho al juego
        public override bool PuedeAtacarA(Ente objetivo)
        {
            Juego?.BuscarBicho();
            return true; // Devuelve true para mantener compatibilidad, ajusta según lógica real
        }
    }
}
