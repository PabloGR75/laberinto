namespace Laberinto.Core.Models
{
    public class Abierta : EstadoPuerta
    {
        public override bool EstaAbierta => true;

        public override void Abrir(Puerta puerta)
        {
            // Ya está abierta, no hace nada.
        }

        public override void Cerrar(Puerta puerta)
        {
            puerta.Estado = new Cerrada();
        }
    }
}
