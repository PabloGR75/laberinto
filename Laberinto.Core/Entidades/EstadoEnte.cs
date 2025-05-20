// EstadoEnte.cs
namespace Laberinto.Core.Entidades
{
    public abstract class EstadoEnte
    {
        public abstract bool EstaVivo { get; }
        public abstract void Actua(Ente ente);
        public abstract void Atacar(Ente ente, Ente objetivo);
    }

    public class Vivo : EstadoEnte
    {
        public override bool EstaVivo => true;

        public override void Actua(Ente ente)
        {
            // Lógica de actuación para entes vivos.
        }

        public override void Atacar(Ente ente, Ente objetivo)
        {
            objetivo.EsAtacadoPor(ente);
        }
    }

    public class Muerto : EstadoEnte
    {
        public override bool EstaVivo => false;

        public override void Actua(Ente ente)
        {
            // Un ente muerto no actúa.
        }

        public override void Atacar(Ente ente, Ente objetivo)
        {
            // Un ente muerto no ataca.
        }
    }
}
