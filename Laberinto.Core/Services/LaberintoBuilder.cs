using Laberinto.Core.Models;

namespace Laberinto.Core.Services
{
    public class LaberintoBuilder
    {
        protected LaberintoObj laberintoActual;

        public virtual void InicializarLaberinto()
        {
            laberintoActual = new LaberintoObj();
        }

        public virtual void ConstruirHabitacion(int num)
        {
            var habitacion = new Habitacion(num);
            laberintoActual.AgregarHijo(habitacion);
        }

        public virtual void ConstruirPuerta(int numHabA, int numHabB)
        {
            var habA = laberintoActual.ObtenerHabitacion(numHabA);
            var habB = laberintoActual.ObtenerHabitacion(numHabB);

            if (habA != null && habB != null)
            {
                var puerta = new Puerta(habA, habB);
                habA.AgregarPuerta(new Este(), puerta); // Orientación de ejemplo
                habB.AgregarPuerta(new Oeste(), puerta); // Orientación de ejemplo
            }
        }

        public virtual LaberintoObj ObtenerLaberinto()
        {
            return laberintoActual;
        }
    }
}
