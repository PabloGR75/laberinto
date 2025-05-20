namespace Laberinto.Core.Models
{
    public class Noroeste : Orientacion
    {
        public override Punto CalcularPosicionDesde(Forma forma)
        {
            // Mueve una celda hacia el noroeste: x -1, y - 1 (o y + 1 si ese es tu convenio)
            return forma.Punto.Mover(-1, -1);
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
