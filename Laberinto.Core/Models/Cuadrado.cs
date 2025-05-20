using System.Collections.Generic;

namespace Laberinto.Core.Models
{
    public class Cuadrado : Forma
    {
        public Punto Punto { get; private set; }
        public Orientacion OrientacionActual { get; private set; }

        public Cuadrado(Punto punto, Orientacion orientacion)
            : base("Cuadrado", 4, new List<Orientacion> { new Norte(), new Sur(), new Este(), new Oeste() }, 2, 2)
        {
            Punto = punto;
            OrientacionActual = orientacion;
        }

        public override Forma Avanzar()
        {
            // Crea una nueva forma cuadrada avanzando en la orientación actual
            var nuevoPunto = OrientacionActual.CalcularPosicionDesde(this);
            return new Cuadrado(nuevoPunto, OrientacionActual);
        }

        public override Forma CambiarOrientacion(Orientacion nuevaOrientacion)
        {
            return new Cuadrado(Punto, nuevaOrientacion);
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
