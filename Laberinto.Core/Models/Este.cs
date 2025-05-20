// Este.cs
namespace Laberinto.Core.Models
{
    public class Este : Orientacion
    {
        public override Punto CalcularPosicionDesde(Forma forma)
        {
            // Mueve una celda hacia el este: x + 1, y igual
            return forma.Punto.Mover(1, 0);
        }

        public override ElementoMapa ObtenerElementoEn(Contenedor contenedor, Forma forma)
        {
            // Aquí deberías tener algún helper en Contenedor para devolver el elemento en la nueva posición
            var puntoDestino = CalcularPosicionDesde(forma);
            return contenedor.ObtenerElementoEnPosicion(puntoDestino);
        }

        public override void PonerElementoEn(Contenedor contenedor, ElementoMapa elemento, Forma forma)
        {
            var puntoDestino = CalcularPosicionDesde(forma);
            contenedor.PonerElementoEnPosicion(elemento, puntoDestino);
        }

        public override void Recorrer(Contenedor contenedor, Forma forma, Action<ElementoMapa> accion)
        {
            var elem = ObtenerElementoEn(contenedor, forma);
            if (elem != null)
                accion(elem);
        }
    }
}
