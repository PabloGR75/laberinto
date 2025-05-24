namespace Laberinto.Core.Entidades
{
    /// <summary>
    /// Estado abstracto del ente: puede ser Vivo o Muerto (State Pattern).
    /// </summary>
    public abstract class EstadoEnte
    {
        // Actuar: por defecto, responsabilidad de la subclase
        public abstract void Actua(Ente unEnte);

        // Atacar: por defecto, responsabilidad de la subclase
        public abstract void Atacar(Ente alguien);

        // ¿Está vivo? Por defecto, falso
        public virtual bool EstaVivo => false;
    }
}
