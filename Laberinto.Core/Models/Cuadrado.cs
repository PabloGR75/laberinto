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

        public override Forma DeepClone()
        {
            var clone = (Cuadrado)base.DeepClone();
            // No clonas los ElementoMapa aquí, porque las habitaciones y demás se clonan por otro lado
            // y luego hay que reconectar las referencias a los clones
            clone.Norte = this.Norte; // Ojo: referencia directa, solo si quieres
            clone.Sur = this.Sur;
            clone.Este = this.Este;
            clone.Oeste = this.Oeste;
            return clone;
        }

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
