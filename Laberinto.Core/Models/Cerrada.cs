namespace Laberinto.Core.Models
{
    public class Cerrada : EstadoPuerta
    {
        public override bool EstaAbierta => false;

        public override void Abrir(Puerta puerta)
        {
            puerta.Estado = new Abierta();
        }

        public override void Cerrar(Puerta puerta)
        {
            // Ya está cerrada, no hace nada.
        }
    }
}
