using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

namespace Laberinto.Core
{
    public class JuegoLaberinto
    {
        public LaberintoObj Laberinto { get; set; }
        public List<Bicho> Bichos { get; set; } = new List<Bicho>();
        public Dictionary<Bicho, object> Hilos { get; set; } // Los hilos pueden ser Tasks si los necesitas
        public Personaje Person { get; set; }
        public LaberintoObj Prototipo { get; set; }

        public JuegoLaberinto()
        {
            Bichos = new List<Bicho>();
            Hilos = new Dictionary<Bicho, object>();
        }

        public JuegoLaberinto(LaberintoObj laberinto) : this()
        {
            Laberinto = laberinto;
        }

        // ---------------------- Puertas ----------------------

        public void AbrirPuertas()
        {
            foreach (var puerta in Laberinto.ObtenerTodasLasPuertas())
            {
                puerta.Abrir();
                Console.WriteLine($"Puerta {puerta} ahora está {puerta.Estado.GetType().Name}");
            }
        }

        public void CerrarPuertas()
        {
            foreach (var puerta in Laberinto.ObtenerTodasLasPuertas())
                puerta.Cerrar();
        }
        public void AbrirTodasLasPuertas()
        {
            foreach (var puerta in Laberinto.ObtenerTodasLasPuertas())
            {
                puerta.Abrir();
            }
            Console.WriteLine("¡Todas las puertas han sido abiertas!");
        }

        public void CerrarTodasLasPuertas()
        {
            foreach (var puerta in Laberinto.ObtenerTodasLasPuertas())
            {
                puerta.Cerrar();
            }
            Console.WriteLine("¡Todas las puertas han sido cerradas!");
        }

        // ---------------------- Bichos ----------------------

        public void AgregarBicho(Bicho bicho)
        {
            Bichos.Add(bicho);
            bicho.Juego = this;
        }

        public void EliminarBicho(Bicho bicho)
        {
            if (!Bichos.Remove(bicho))
                Console.WriteLine("No existe ese bicho");
        }

        public void LanzarBicho(Bicho bicho)
        {
            // Versión simplificada, sin hilos. Puedes ampliar luego.
            Console.WriteLine($"{bicho} se activa");
            while (bicho.EstaVivo())
            {
                bicho.Actua();
            }
            Hilos[bicho] = null; // placeholder, usar Task si lo necesitas
        }

        public bool TodosLosBichosMuertos()
        {
            return Bichos != null && Bichos.All(b => !b.EstaVivo());
        }

        public void LanzarBichos()
        {
            foreach (var bicho in Bichos)
                LanzarBicho(bicho);
        }

        // ---------------------- Personaje ----------------------

        public void AgregarPersonaje(string nombre)
        {
            Person = new Personaje(nombre);

            var habInicial = Laberinto.ObtenerHabitacion(1); // O usa la lógica que prefieras

            if (habInicial == null)
                throw new InvalidOperationException("No se encontró la habitación inicial (1) en el laberinto.");

            Person.Posicion = habInicial;
            // Realmente "entra" en la habitación (y ejecute lógica de entrar):
            habInicial?.Entrar(Person);

            Person.Juego = this;
            Laberinto?.Entrar(Person);
        }

        // ---------------------- Ataques y control de juego ----------------------

        public void EjecutarTurnoBichos()
        {
            Console.WriteLine("\nPresiona ENTER para que los bichos ejecuten sus turnos...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------");

            var random = new Random();
            foreach (var bicho in Bichos.Where(b => b.EstaVivo()))
            {
                // 50% de probabilidad de moverse o atacar
                if (random.Next(2) == 0)
                {
                    // Moverse
                    var orientaciones = bicho.Posicion?.Puertas.Keys.ToList();
                    if (orientaciones?.Count > 0)
                    {
                        var orientacion = orientaciones[random.Next(orientaciones.Count)];
                        var puerta = bicho.Posicion.Puertas[orientacion];

                        if (puerta.EstaAbierta())
                        {
                            var habitacionDestino = puerta.OtroLado(bicho.Posicion) as Habitacion;
                            if (habitacionDestino != null)
                            {
                                bicho.Posicion = habitacionDestino;
                                Console.WriteLine($"- Bicho {bicho.Modo?.GetType().Name} se movió a Habitación {habitacionDestino.Num}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"- Bicho {bicho.Modo?.GetType().Name} intentó moverse pero la puerta estaba cerrada");
                        }
                    }
                }
                else
                {
                    // Atacar (si está en la misma habitación que el personaje)
                    if (bicho.Posicion == Person.Posicion && !Person.EstaEscondido)
                    {
                        int danno = bicho.Vidas; // El bicho ataca con sus vidas como daño
                        Person.RecibirDanno(danno);
                        Console.WriteLine($"- ¡El bicho {bicho.Modo?.GetType().Name} te atacó y te quitó {danno} vidas!");
                    }
                    else
                    {
                        Console.WriteLine($"- El bicho {bicho.Modo?.GetType().Name} decidió atacar pero no había nadie en la habitación");
                    }
                }
            }
            Console.WriteLine("----------------------------------------------------------");
        }

        public void BuscarBicho()
        {
            var posPerson = Person?.Posicion;
            var bicho = Bichos.Find(b => b.EstaVivo() && b.Posicion == posPerson);
            if (bicho != null)
                bicho.EsAtacadoPor(Person);
        }

        public void BuscarPersonaje(Bicho unBicho)
        {
            if (Person?.EstaEscondido ?? false) return;

            var posBicho = unBicho.Posicion;
            var posPerson = Person?.Posicion;
            if (posBicho == posPerson && Person != null)
                Person.EsAtacadoPor(unBicho);
        }

        public void TerminarBicho(Bicho unBicho)
        {
            unBicho.Vidas = 0;
            //Console.WriteLine($"{unBicho} muere");
            EstanTodosLosBichosMuertos();
        }

        public void TerminarBichos()
        {
            foreach (var bicho in Bichos)
                TerminarBicho(bicho);
        }

        public void MuerePersonaje()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Fin del juego: Ganan los bichos");
            TerminarBichos();
        }

        public void GanaPersonaje()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Fin del juego: gana el personaje");
        }

        public void EstanTodosLosBichosMuertos()
        {
            var algunoVivo = Bichos.Exists(b => b.EstaVivo());
            if (!algunoVivo && (Person?.EstaVivo() ?? false))
                GanaPersonaje();
        }

        // ---------------------- Prototipo/Laberintos ----------------------

        public LaberintoObj ClonarLaberinto()
        {
            // return Prototipo?.VeryDeepCopy();
            //throw new NotImplementedException("ClonarLaberinto: implementar deep copy");
            return Prototipo.DeepClone() as LaberintoObj;
        }

        public LaberintoObj CrearNuevoLaberinto()
        {
            // Aquí deberías crear un laberinto nuevo a partir del prototipo
            // throw new NotImplementedException("CrearNuevoLaberinto: implementar según tu lógica");
            return Prototipo.DeepClone() as LaberintoObj;
        }

        public Habitacion ObtenerHabitacion(int num)
        {
            return Laberinto?.ObtenerHabitacion(num);
        }
    }
}
