using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    public class Agresivo : Modo
    {
        public override void Dormir(Bicho unBicho)
        {
            // No duerme: los bichos agresivos siempre actúan
        }

        public override bool EsAgresivo() => true;

        // Puedes personalizar el atacar o caminar si necesitas lógica distinta
    }
}
