using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core.Models
{
    public class Rombo : Forma
    {
        public ElementoMapa Noreste { get; set; }
        public ElementoMapa Noroeste { get; set; }
        public ElementoMapa Sureste { get; set; }
        public ElementoMapa Suroeste { get; set; }

        public void IrAlNoreste(Ente alguien)
        {
            Noreste?.Entrar(alguien);
        }
        public void IrAlNoroeste(Ente alguien)
        {
            Noroeste?.Entrar(alguien);
        }
        public void IrAlSureste(Ente alguien)
        {
            Sureste?.Entrar(alguien);
        }
        public void IrAlSuroeste(Ente alguien)
        {
            Suroeste?.Entrar(alguien);
        }
    }
}
