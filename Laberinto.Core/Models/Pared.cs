using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Pared : Hoja
    {
        public override bool EsPared => true;

        public override void Entrar(Ente quien)
        {
            // No se puede entrar en una pared.
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitPared(this);
        }
    }
}
