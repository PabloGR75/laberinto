using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Cuadro : ElementoMapa
    {
        public string Descripcion { get; set; } = "Un cuadro antiguo";

        public override bool EsCuadro => true;

        public override void Entrar(Ente quien)
        {
            // Elemento estático - no hace nada
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitCuadro(this);
        }

        public override string ToString()
        {
            return $"Cuadro: {Descripcion}";
        }
    }
}