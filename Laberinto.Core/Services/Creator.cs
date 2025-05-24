using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Services
{
    /// Implementa el patrón Factory Method para construir elementos del laberinto.
    public class Creator
    {
        // ---- Métodos de cambio de modo ----
        public virtual void CambiarAModoAgresivo(Bicho unBicho)
        {
            unBicho.Modo = new Agresivo();
            unBicho.Poder = 10;
        }

        // ---- Factory Method para Bichos ----
        public virtual Bicho FabricarBichoAgresivo()
        {
            var bicho = new Bicho(new Agresivo())
            {
                Vidas = 5,
                Poder = 5
            };
            return bicho;
        }

        public virtual Bicho FabricarBichoAgresivo(Habitacion unaHab)
        {
            var bicho = FabricarBichoAgresivo();
            bicho.EntrarEn(unaHab);
            return bicho;
        }

        public virtual Bicho FabricarBichoPerezoso()
        {
            var bicho = new Bicho(new Perezoso())
            {
                Poder = 1,
                Vidas = 1
            };
            return bicho;
        }

        public virtual Bicho FabricarBichoPerezoso(Habitacion unaHab)
        {
            var bicho = FabricarBichoPerezoso();
            bicho.EntrarEn(unaHab);
            return bicho;
        }

        // ---- Factory Method para bomba ----
        public virtual Bomba FabricarBomba()
        {
            return new Bomba();
        }

        // ---- Factory Method para habitación ----
        public virtual Habitacion FabricarHabitacion(int num)
        {
            var hab = new Habitacion(num)
            {
                Forma = new Cuadrado()
            };

            // Añade orientaciones y paredes
            hab.AgregarOrientacion(FabricarNorte());
            hab.AgregarOrientacion(FabricarSur());
            hab.AgregarOrientacion(FabricarEste());
            hab.AgregarOrientacion(FabricarOeste());

            foreach (var orientacion in hab.ObtenerOrientaciones())
            {
                hab.PonerEnOr(orientacion, FabricarPared());
            }

            return hab;
        }

        // ---- Factory Method para juego y laberinto ----
        public virtual JuegoLaberinto FabricarJuego(LaberintoObj laberinto = null)
        {
            return laberinto != null ? new JuegoLaberinto(laberinto) : new JuegoLaberinto(new LaberintoObj());
        }

        public virtual LaberintoObj FabricarLaberinto()
        {
            return new LaberintoObj();
        }

        // ---- Factory Method para pared ----
        public virtual Pared FabricarPared()
        {
            return new Pared();
        }

        // ---- Factory Method para puerta ----
        public virtual Puerta FabricarPuerta()
        {
            return new Puerta();
        }

        // ---- Métodos para orientaciones ----
        public virtual Orientacion FabricarNorte() => Norte.Instancia;
        public virtual Orientacion FabricarSur() => Sur.Instancia;
        public virtual Orientacion FabricarEste() => Este.Instancia;
        public virtual Orientacion FabricarOeste() => Oeste.Instancia;

        public virtual Orientacion FabricarNoreste() => Noreste.Instancia;
        public virtual Orientacion FabricarNoroeste() => Noroeste.Instancia;
        public virtual Orientacion FabricarSureste() => Sureste.Instancia;
        public virtual Orientacion FabricarSuroeste() => Suroeste.Instancia;

    }
}
