using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core.Services
{
    public class ComandoAbrir : Comando
    {
        private readonly Puerta _puerta;

        public ComandoAbrir(Ente ente, Puerta puerta) : base(ente)
        {
            _puerta = puerta;
        }

        public override void Ejecutar()
        {
            _puerta.Abrir();
        }
    }
}
