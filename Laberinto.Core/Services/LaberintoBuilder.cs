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

        public virtual void FabricarLaberinto()
        {
            InicializarLaberinto();
        }

        public virtual object FabricarHabitacion(int num)
        {
            var habitacion = new Habitacion(num);
            laberintoActual.AgregarHijo(habitacion);
            return habitacion;
        }

        public object FabricarArmario(int numArmario, object padre)
        {
            var habitacion = padre as Habitacion;
            // Obtener la habitación de destino
            if (habitacion == null)
                throw new InvalidOperationException("El objeto pasado como padre no es una Habitación válida.");

            // Crear el armario (se añade automáticamente como hijo de la habitación)
            var armario = new Armario(numArmario, habitacion);

            // Si quieres, puedes devolver el armario por si necesitas más operaciones
            return armario;
        }

        public virtual void FabricarPuerta(int numHabA, int numHabB)
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

        public virtual void FabricarPuertaL1(int numHabA, string orA, int numHabB, string orB)
        {
            // Asumiendo que orientación son strings tipo "Norte", "Sur", etc.
            var habA = laberintoActual.ObtenerHabitacion(numHabA);
            var habB = laberintoActual.ObtenerHabitacion(numHabB);
            if (habA != null && habB != null)
            {
                var puerta = new Puerta(habA, habB);

                // Obtener la orientación (aquí necesitarías una función auxiliar)
                var orientA = ObtenerOrientacionDesdeString(orA);
                var orientB = ObtenerOrientacionDesdeString(orB);

                habA.AgregarPuerta(orientA, puerta);
                habB.AgregarPuerta(orientB, puerta);
            }
        }

        public virtual void FabricarBombaEn(Contenedor contenedor)
        {
            var bomba = new Bomba();
            contenedor.AgregarHijo(bomba);
        }

        public virtual void FabricarTunelEn(Contenedor contenedor)
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

        public virtual void FabricarFormaCuadrada(Contenedor contenedor)
        {
            var forma = new Cuadrado();
            forma.AgregarOrientacion(Norte.Instancia);
            forma.AgregarOrientacion(Sur.Instancia);
            forma.AgregarOrientacion(Este.Instancia);
            forma.AgregarOrientacion(Oeste.Instancia);
            contenedor.Forma = forma;
        }

        public virtual void FabricarFormaRombo(Contenedor contenedor)
        {
            var forma = new Rombo();
            forma.AgregarOrientacion(Noreste.Instancia);
            forma.AgregarOrientacion(Noroeste.Instancia);
            forma.AgregarOrientacion(Sureste.Instancia);
            forma.AgregarOrientacion(Suroeste.Instancia);
            contenedor.Forma = forma;
        }

        public void FabricarBichoAgresivo(int numHabitacion)
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

        public void FabricarBichoPerezoso(int numHabitacion)
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

        public virtual void FabricarBichoModo(string modo, int posicion)
        {
            if (modo.ToLower() == "agresivo")
                FabricarBichoAgresivo(posicion);
            else
                FabricarBichoPerezoso(posicion);
        }

        public virtual LaberintoObj ObtenerLaberinto()
        {
            return laberintoActual;
        }

        public virtual JuegoLaberinto ObtenerJuego()
        {
            return juegoActual;
        }

        // Helper para obtener orientación
        private Orientacion ObtenerOrientacionDesdeString(string nombre)
        {
            switch (nombre.ToLower())
            {
                case "norte": return Norte.Instancia;
                case "sur": return Sur.Instancia;
                case "este": return Este.Instancia;
                case "oeste": return Oeste.Instancia;
                case "noreste": return Noreste.Instancia;
                case "noroeste": return Noroeste.Instancia;
                case "sureste": return Sureste.Instancia;
                case "suroeste": return Suroeste.Instancia;
                default: throw new ArgumentException($"Orientación desconocida: {nombre}");
            }
        }
    }
}
