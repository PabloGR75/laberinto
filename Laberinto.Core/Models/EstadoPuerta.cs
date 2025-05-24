namespace Laberinto.Core.Models
{
    // Interfaz de los estados de la Puerta
    public abstract class EstadoPuerta
    {
        public abstract void Abrir(Puerta unaPuerta);
        public abstract void Cerrar(Puerta unaPuerta);
        public abstract void Entrar(Entidades.Ente alguien, Puerta unaPuerta);
        public virtual bool EstaAbierta() => false;
    }
}

