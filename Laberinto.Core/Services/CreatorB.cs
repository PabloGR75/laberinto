using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Services
{
    /// <summary>
    /// Versión de Creator que crea paredes bomba en vez de paredes normales.
    /// </summary>
    public class CreatorB : Creator
    {
        public override Pared FabricarPared()
        {
            return new ParedBomba();
        }
    }
}
