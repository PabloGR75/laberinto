using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core.Models
{
    public class Cuadrado : Forma
    {
        public ElementoMapa Norte { get; set; }
        public ElementoMapa Sur { get; set; }
        public ElementoMapa Este { get; set; }
        public ElementoMapa Oeste { get; set; }

        public override void IrAlNorte(Ente alguien)
        {
            Norte?.Entrar(alguien);
        }
        public void IrAlSur(Ente alguien)
        {
            Sur?.Entrar(alguien);
        }
        public void IrAlEste(Ente alguien)
        {
            Este?.Entrar(alguien);
        }
        public void IrAlOeste(Ente alguien)
        {
            Oeste?.Entrar(alguien);
        }
    }
}
