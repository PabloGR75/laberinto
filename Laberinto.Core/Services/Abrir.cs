using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    /// Comando para abrir un elemento del mapa (ej: Puerta).
    public class Abrir : Comando
    {
        public Abrir(ElementoMapa receptor) : base(receptor) { }

        public override void Ejecutar(Ente quien)
        {
            // Llama al método Abrir del receptor si está definido
            // Puedes hacer un cast seguro si sabes que es Puerta, o usar reflexión/dinámico
            if (Receptor is Puerta puerta)
                puerta.Abrir();
        }
    }
}
