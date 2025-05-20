using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Bomba : Decorator
    {
        private bool explotada;

        public Bomba(ElementoMapa componente) : base(componente)
        {
            explotada = false;
        }

        public override bool EsBomba => true;

        public override void Entrar(Ente quien)
        {
            Explotar(quien);
            base.Entrar(quien);
        }

        void Explotar(Ente quien)
        {
            if (!explotada)
            {
                quien.HeMuerto();
                explotada = true;
            }
        }

        public bool Explotada => explotada;

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitBomba(this);
        }
    }
}
