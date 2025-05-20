using System.Collections.Generic;

namespace Laberinto.Core.Models
{
    public class Rombo : Forma
    {
        public Punto Punto { get; private set; }
        public Orientacion OrientacionActual { get; private set; }

        public Rombo(Punto punto, Orientacion orientacion)
            : base(
                "Rombo",
                4,
                new List<Orientacion>
                {
                    new Noreste(),
                    new Noroeste(),
                    new Sureste(),
                    new Suroeste()
                },
                2, 2 // O el tamaño (ancho, alto) adecuado para tu modelo
            )
        {
            Punto = punto;
            OrientacionActual = orientacion;
        }

        public override Forma Avanzar()
        {
            var nuevoPunto = OrientacionActual.CalcularPosicionDesde(this);
            return new Rombo(nuevoPunto, OrientacionActual);
        }

        public override Forma CambiarOrientacion(Orientacion nuevaOrientacion)
        {
            return new Rombo(Punto, nuevaOrientacion);
        }

        public override Punto CalcularPosicion(Orientacion orientacion)
        {
            return orientacion.CalcularPosicionDesde(this);
        }

        public override void PonerEnOr(Contenedor contenedor, Orientacion orientacion, ElementoMapa elemento)
        {
            var puntoDestino = orientacion.CalcularPosicionDesde(this);
            contenedor.PonerElementoEnPosicion(elemento, puntoDestino);
        }
    }
}
