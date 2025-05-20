namespace Laberinto.Core.Models
{
    public class Oeste : Orientacion
    {
        public override Punto CalcularPosicionDesde(Forma forma)
        {
            // Mueve una celda hacia el oeste: x - 1, y igual
            return forma.Punto.Mover(-1, 0);
        }

        public override ElementoMapa ObtenerElementoEn(Contenedor contenedor, Forma forma)
        {
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
