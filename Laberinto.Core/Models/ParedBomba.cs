using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class ParedBomba : Pared
    {
        private bool activa;

        public ParedBomba()
        {
            activa = true;
        }

        public override void Entrar(Ente quien)
        {
            if (activa)
            {
                quien.HeMuerto();
                activa = false;
            }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitPared(this);
        }
    }
}
