using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    /// El laberinto, contenedor de habitaciones y demás elementos.

    public class LaberintoObj : Contenedor
    {
        /// Devuelve la habitación número 1 (puedes ampliar para buscar por número).
        public Habitacion ObtenerHabitacion(int num)
        {
            foreach (var hijo in Hijos)
            {
                if (hijo is Habitacion hab && hab.Num == num)
                    return hab;
            }
            return null;
        }

        /// Hace que un ente entre en la habitación 1.
        public override void Entrar(Ente quien)
        {
            var hab1 = ObtenerHabitacion(1);
            hab1?.Entrar(quien);
        }

        /// Permite recorrer el laberinto aplicando un bloque de acción.
        public override void Recorrer(Action<ElementoMapa> bloque)
        {
            bloque(this);
            foreach (var hijo in Hijos)
                hijo.Recorrer(bloque);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitLaberinto(this);
        }
    }
}
