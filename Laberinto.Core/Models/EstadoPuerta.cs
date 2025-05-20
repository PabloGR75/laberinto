namespace Laberinto.Core.Models
{
    public abstract class EstadoPuerta
    {
        public abstract bool EstaAbierta { get; }
        public abstract void Abrir(Puerta puerta);
        public abstract void Cerrar(Puerta puerta);
    }
}
