using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core.Services
{
    public class ComandoMover : Comando
    {
        private readonly Orientacion _orientacion;

        public ComandoMover(Bicho bicho, Orientacion orientacion) : base(bicho)
        {
            _orientacion = orientacion;
        }

        public override void Ejecutar()
        {
            if (Receptor is Bicho bicho)
                bicho.Mover(_orientacion);
        }
    }
}