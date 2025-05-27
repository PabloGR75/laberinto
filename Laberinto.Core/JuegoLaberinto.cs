// JuegoLaberinto.cs
using System;
using System.Collections.Generic;
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

        // ---- Puertas ----
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

        // ---- Bichos ----
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

        // ---- Personaje ----
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

        // ---- Ataques y control de juego ----
        public void BuscarBicho()
        {
            var posPerson = Person?.Posicion;
            var bicho = Bichos.Find(b => b.EstaVivo() && b.Posicion == posPerson);
            if (bicho != null)
                bicho.EsAtacadoPor(Person);
        }

        public void BuscarPersonaje(Bicho unBicho)
        {
            var posBicho = unBicho.Posicion;
            var posPerson = Person?.Posicion;
            if (posBicho == posPerson && Person != null)
                Person.EsAtacadoPor(unBicho);
        }

        public void TerminarBicho(Bicho unBicho)
        {
            unBicho.Vidas = 0;
            Console.WriteLine($"{unBicho} muere");
            EstanTodosLosBichosMuertos();
        }

        public void TerminarBichos()
        {
            foreach (var bicho in Bichos)
                TerminarBicho(bicho);
        }

        public void MuerePersonaje()
        {
            Console.WriteLine("Fin del juego: Ganan los bichos");
            TerminarBichos();
        }

        public void GanaPersonaje()
        {
            Console.WriteLine("Fin del juego: gana el personaje");
        }

        public void EstanTodosLosBichosMuertos()
        {
            var algunoVivo = Bichos.Exists(b => b.EstaVivo());
            if (!algunoVivo && (Person?.EstaVivo() ?? false))
                GanaPersonaje();
        }

        // ---- Prototipo/Laberintos ----
        public LaberintoObj ClonarLaberinto()
        {
            // ¡ATENCIÓN! Esto es un placeholder: implementar deep copy real
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

        // public LaberintoObj CrearLaberinto2Habitaciones()
        // {
        //     // Ejemplo de cómo crearlo, adaptar según tu estructura
        //     var hab1 = new Habitacion(1);
        //     var hab2 = new Habitacion(2);
        //     var puerta = new Puerta();

        //     hab1.Este = new Pared();
        //     hab1.Oeste = new Pared();
        //     hab1.Norte = new Pared();
        //     hab1.Sur = puerta;

        //     hab2.Este = new Pared();
        //     hab2.Oeste = new Pared();
        //     hab2.Sur = new Pared();
        //     hab2.Norte = puerta;

        //     puerta.Lado1 = hab1;
        //     puerta.Lado2 = hab2;

        //     var lab = new LaberintoObj();
        //     lab.AgregarHabitacion(hab1);
        //     lab.AgregarHabitacion(hab2);
        //     Laberinto = lab;
        //     return lab;
        // }

        public Habitacion ObtenerHabitacion(int num)
        {
            return Laberinto?.ObtenerHabitacion(num);
        }
    }
}
