using Laberinto.Core.Models;
using System;

namespace Laberinto.Core.Entidades
{
    public class Perezoso : Modo
    {
        public override void Dormir(Bicho unBicho)
        {
            // 50% de las veces el bicho perezoso duerme y no actúa
            if (new Random().NextDouble() < 0.5)
            {
                // El bicho "duerme" y no sigue con la acción
                return;
            }
        }

        public override bool EsPerezoso() => true;
    }
}
