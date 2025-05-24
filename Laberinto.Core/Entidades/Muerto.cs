namespace Laberinto.Core.Entidades
{
    /// <summary>
    /// Estado: Muerto. No puede actuar ni atacar.
    /// </summary>
    public class Muerto : EstadoEnte
    {
        public override void Actua(Ente unEnte)
        {
            // Los muertos no actúan
        }

        public override void Atacar(Ente alguien)
        {
            // Los muertos no atacan
        }

        public override bool EstaVivo => false;
    }
}
