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
            Poder = 3;
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

        public string AtacarEnHabitacionActual(JuegoLaberinto juego)
        {
            if (this.Posicion is Habitacion hab)
            {
                // Busca bichos en la habitación actual usando el método del juego
                var bichosEnHabitacion = juego.Bichos
                    .Where(b => b.Posicion == hab && b.EstaVivo())
                    .ToList();

                if (!bichosEnHabitacion.Any())
                    return "No hay ningún bicho vivo en esta habitación para atacar.";

                // Ataca al primer bicho encontrado (o puedes modificar para atacar a todos)
                var bicho = bichosEnHabitacion.First();
                int danno = this.Poder;
                bicho.RecibirDanno(danno);

                string resultado = $"¡Atacas al bicho {bicho.Modo?.GetType().Name} y le causas {danno} de daño!";
                if (!bicho.EstaVivo())
                {
                    resultado += "\n¡Has derrotado al bicho!";
                }

                return resultado;
            }
            return "No estás en una habitación.";
        }

        public void RecibirDanno(int danno)
        {
            this.Vidas -= danno;
            if (this.Vidas <= 0)
            {
                this.Vidas = 0;
                this.HeMuerto();
            }
            //Console.WriteLine($"[DEBUG] Personaje recibe {danno} de daño. Vidas restantes: {this.Vidas}");
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

        public override bool EstaVivo()
        {
            return Vidas > 0;
        }

        public string MoverA(Orientacion orientacion)
        {
            // Suponiendo que Posicion es la habitación actual (Habitacion)
            var habitacionActual = this.Posicion as Habitacion;
            if (habitacionActual == null)
                return "Error: No estás en una habitación válida.";

            // Busca la puerta en esa orientación
            if (!habitacionActual.Puertas.TryGetValue(orientacion, out var puerta))
                return $"Hay una pared en dirección {orientacion}. Te quedas en Habitación {habitacionActual.Num}.";

            // Hay puerta
            if (!puerta.EstaAbierta())
                return $"Chocas con una puerta cerrada dirección {orientacion}. Te quedas en Habitación {habitacionActual.Num}.";

            // La puerta está abierta, puedes cruzar
            // Averigua a qué habitación te mueves (suponiendo método OtroLado)
            var otra = puerta.OtroLado(habitacionActual) as Habitacion;
            if (otra != null)
            {
                this.Posicion = otra;
                return $"Cruzas la puerta {orientacion} y entras en Habitación {otra.Num}.";
            }

            return "Error: No se pudo mover a la otra habitación.";
        }

    }
}
