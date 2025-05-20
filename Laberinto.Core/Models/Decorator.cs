using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public abstract class Decorator : ElementoMapa
    {
        protected ElementoMapa Componente { get; set; }

        protected Decorator(ElementoMapa componente)
        {
            Componente = componente;
        }

        public override bool EsDecorador => true;

        public override void Entrar(Ente quien)
        {
            if (Componente != null)
                Componente.Entrar(quien);
        }

        public override void Accept(IVisitor visitor)
        {
            if (Componente != null)
                Componente.Accept(visitor);
        }
    }
}

