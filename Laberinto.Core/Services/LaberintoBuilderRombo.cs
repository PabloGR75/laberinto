using Laberinto.Core.Models;

namespace Laberinto.Core.Services
{
    public class LaberintoBuilderRombo : LaberintoBuilder
    {
        public override Forma FabricarForma()
        {
            var forma = new Rombo();
            forma.AgregarOrientacion(FabricarNoreste());
            forma.AgregarOrientacion(FabricarSureste());
            forma.AgregarOrientacion(FabricarNoroeste());
            forma.AgregarOrientacion(FabricarSuroeste());
            return forma;
        }

        public virtual Orientacion FabricarNoreste() => Noreste.Instancia;
        public virtual Orientacion FabricarNoroeste() => Noroeste.Instancia;
        public virtual Orientacion FabricarSureste() => Sureste.Instancia;
        public virtual Orientacion FabricarSuroeste() => Suroeste.Instancia;
    }
}
