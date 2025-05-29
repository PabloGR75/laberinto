using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Lampara : ElementoMapa
    {
        public override bool EsLampara => true;

        public override void Entrar(Ente quien)
        {
            // Elemento estático - no hace nada
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitLampara(this);
        }

        public override string ToString()
        {
            return "Lámpara";
        }
    }
}