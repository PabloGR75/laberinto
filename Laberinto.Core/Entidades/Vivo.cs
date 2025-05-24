namespace Laberinto.Core.Entidades
{
    /// Estado: Vivo. Puede actuar y atacar.
    public class Vivo : EstadoEnte
    {
        public override void Actua(Ente unEnte)
        {
            // Cuando está vivo, delega en la estrategia (modo)
            unEnte.PuedeActuar();
        }

        public override void Atacar(Ente unEnte)
        {
            unEnte.PuedeAtacar();
        }

        public override bool EstaVivo => true;
    }
}
