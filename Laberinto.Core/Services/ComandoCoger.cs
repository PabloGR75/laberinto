using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core.Services
{
    public class ComandoCoger : Comando
    {
        private readonly Tesoro _tesoro;

        public ComandoCoger(Ente ente, Tesoro tesoro) : base(ente)
        {
            _tesoro = tesoro;
        }

        public override void Ejecutar()
        {
            // Solo si tu clase Ente tiene un método Coger(Tesoro)
            // Receptor.Coger(_tesoro);
        }
    }
}
