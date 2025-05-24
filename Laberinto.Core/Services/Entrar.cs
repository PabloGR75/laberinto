using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    /// Comando para entrar en un elemento del mapa.
    public class Entrar : Comando
    {
        public Entrar(ElementoMapa receptor) : base(receptor) { }

        public override void Ejecutar(Ente quien)
        {
            Receptor.Entrar(quien);
        }
    }
}
