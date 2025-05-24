namespace Laberinto.Core.Models
{
    public class ParedBomba : Pared
    {
        // Variable de instancia que se llama activa
        public bool Activa { get; set; } = true;

        public override string ToString()
        {
            return "ParedBomba";
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitParedBomba(this);
        }
    }
}
