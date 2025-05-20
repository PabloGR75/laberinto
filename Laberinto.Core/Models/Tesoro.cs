using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Tesoro : Decorator
    {
        private bool disponible;

        public Tesoro(ElementoMapa componente) : base(componente)
        {
            disponible = true;
        }

        public override bool EsTesoro => true;

        public override void Entrar(Ente quien)
        {
            if (disponible)
            {
                Recolectar(quien);
                disponible = false;
            }
            base.Entrar(quien);
        }

        void Recolectar(Ente quien)
        {
            // Añade lógica de recompensa aquí si lo deseas.
        }

        public bool Disponible => disponible;

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitTesoro(this);
        }
    }
}
