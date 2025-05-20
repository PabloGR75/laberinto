using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core.Services
{
    public class ComandoEntrar : Comando
    {
        private readonly ElementoMapa _elemento;

        public ComandoEntrar(Ente ente, ElementoMapa elemento) : base(ente)
        {
            _elemento = elemento;
        }

        public override void Ejecutar()
        {
            _elemento.Entrar(Receptor);
        }
    }
}
