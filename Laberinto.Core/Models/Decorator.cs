using System;

namespace Laberinto.Core.Models
{
    // Decorator es una Hoja que envuelve (decora) un ElementoMapa
    public abstract class Decorator : Hoja
    {
        public ElementoMapa EM { get; set; }

        protected Decorator() { }
        protected Decorator(ElementoMapa em) { EM = em; }
    }
}
