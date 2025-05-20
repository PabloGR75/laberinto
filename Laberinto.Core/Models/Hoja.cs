using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Hoja : ElementoMapa
    {
        // En Smalltalk, entrar: no hace nada.
        public override void Entrar(Ente quien)
        {
            // No hace nada por defecto.
        }

        // Para el patrón Visitor.
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitHoja(this); // Recuerda añadir VisitHoja en IVisitor.
        }
    }
}

