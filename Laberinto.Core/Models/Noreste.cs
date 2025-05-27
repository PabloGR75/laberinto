using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public sealed class Noreste : Orientacion
    {
        private static readonly Noreste _instancia = new Noreste();
        public static Noreste Instancia => _instancia;
        private Noreste() { }

        public override string Nombre => "Noreste";

        public override Punto CalcularPosicionDesde(Forma forma)
        {
            var punto = forma.Punto;
            var nuevoPunto = new Punto(punto.X + 1, punto.Y - 1);
            return nuevoPunto;
        }

        public override void Caminar(Bicho unBicho)
        {
            var pos = unBicho.Posicion;
            pos?.IrAlNoreste(unBicho);
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
