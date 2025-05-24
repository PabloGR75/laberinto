using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Este : Orientacion
    {
        private static readonly Este _instancia = new Este();
        public static Este Instancia => _instancia;
        private Este() { }

        public override Punto CalcularPosicionDesde(Forma forma)
        {
            var punto = forma.Punto;
            var nuevoPunto = new Punto(punto.X + 1, punto.Y);
            return nuevoPunto;
        }

        public override void Caminar(Bicho unBicho)
        {
            var pos = unBicho.Posicion;
            pos?.IrAlEste(unBicho);
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
