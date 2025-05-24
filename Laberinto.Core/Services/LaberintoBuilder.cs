using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Services
{
    public class LaberintoBuilder
    {
        protected LaberintoObj laberintoActual;
        protected JuegoLaberinto juegoActual;

        public virtual void InicializarLaberinto()
        {
            laberintoActual = new LaberintoObj();
        }

        public virtual void InicializarJuego()
        {
            juegoActual = new JuegoLaberinto(laberintoActual);
        }

        public virtual void ConstruirHabitacion(int num)
        {
            var habitacion = new Habitacion(num);
            laberintoActual.AgregarHijo(habitacion);
        }

        public void ConstruirArmario(int numArmario, int numHabitacion)
        {
            // Obtener la habitación de destino
            var habitacion = laberintoActual.ObtenerHabitacion(numHabitacion);
            if (habitacion == null)
                throw new InvalidOperationException($"Habitación {numHabitacion} no existe.");

            // Crear el armario (se añade automáticamente como hijo de la habitación)
            var armario = new Armario(numArmario, habitacion);

            // Si quieres, puedes devolver el armario por si necesitas más operaciones
            // return armario;
        }

        public virtual void ConstruirPuerta(int numHabA, int numHabB)
        {
            var habA = laberintoActual.ObtenerHabitacion(numHabA);
            var habB = laberintoActual.ObtenerHabitacion(numHabB);

            if (habA != null && habB != null)
            {
                var puerta = new Puerta(habA, habB);
                habA.AgregarPuerta(Este.Instancia, puerta); // Orientación ejemplo
                habB.AgregarPuerta(Oeste.Instancia, puerta);
            }
        }

        public virtual void ConstruirBombaEn(Contenedor contenedor)
        {
            var bomba = new Bomba();
            contenedor.AgregarHijo(bomba);
        }

        public virtual void ConstruirTunelEn(Contenedor contenedor)
        {
            var tunel = new Tunel();
            contenedor.AgregarHijo(tunel);
        }

        public virtual void FabricarJuego()
        {
            juegoActual = new JuegoLaberinto();
            juegoActual.Prototipo = laberintoActual;
            juegoActual.Laberinto = juegoActual.ClonarLaberinto();
        }

        public virtual Forma FabricarForma()
        {
            // Por defecto, crea un Cuadrado con las 4 orientaciones clásicas
            var forma = new Cuadrado();
            forma.AgregarOrientacion(Norte.Instancia);
            forma.AgregarOrientacion(Sur.Instancia);
            forma.AgregarOrientacion(Este.Instancia);
            forma.AgregarOrientacion(Oeste.Instancia);
            return forma;
        }

        public virtual void ConstruirFormaCuadrada(Contenedor contenedor)
        {
            var forma = new Cuadrado();
            forma.AgregarOrientacion(Norte.Instancia);
            forma.AgregarOrientacion(Sur.Instancia);
            forma.AgregarOrientacion(Este.Instancia);
            forma.AgregarOrientacion(Oeste.Instancia);
            contenedor.Forma = forma;
        }

        public virtual void ConstruirFormaRombo(Contenedor contenedor)
        {
            var forma = new Rombo();
            forma.AgregarOrientacion(Noreste.Instancia);
            forma.AgregarOrientacion(Noroeste.Instancia);
            forma.AgregarOrientacion(Sureste.Instancia);
            forma.AgregarOrientacion(Suroeste.Instancia);
            contenedor.Forma = forma;
        }

        public void ConstruirBichoAgresivo(int numHabitacion)
        {
            var habitacion = laberintoActual.ObtenerHabitacion(numHabitacion);
            if (habitacion == null)
                throw new InvalidOperationException($"Habitación {numHabitacion} no existe.");

            var bicho = new Bicho(new Agresivo());
            bicho.Vidas = 5;
            bicho.Poder = 5;
            habitacion.Entrar(bicho);
            // Aquí podrías añadirlo a la colección de bichos del juego si hace falta
        }

        public void ConstruirBichoPerezoso(int numHabitacion)
        {
            var habitacion = laberintoActual.ObtenerHabitacion(numHabitacion);
            if (habitacion == null)
                throw new InvalidOperationException($"Habitación {numHabitacion} no existe.");

            var bicho = new Bicho(new Perezoso());
            bicho.Vidas = 1;
            bicho.Poder = 1;
            habitacion.Entrar(bicho);
            // Aquí podrías añadirlo a la colección de bichos del juego si hace falta
        }

        public virtual LaberintoObj ObtenerLaberinto()
        {
            return laberintoActual;
        }

        public virtual JuegoLaberinto ObtenerJuego()
        {
            return juegoActual;
        }
    }
}
