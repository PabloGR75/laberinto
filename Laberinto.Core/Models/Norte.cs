using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Norte : Orientacion
    {
        // --- Singleton (equivalente a "default" en Smalltalk) ---
        private static readonly Norte _instancia = new Norte();
        public static Norte Instancia => _instancia;

        // Privado para prohibir la creación con new
        private Norte() { }

        // --- Calcular la nueva posición ---
        public override Punto CalcularPosicionDesde(Forma forma)
        {
            // En Smalltalk: x igual, y - 1
            var punto = forma.Punto;
            var nuevoPunto = new Punto(punto.X, punto.Y - 1);
            // Puedes delegar, como en Smalltalk:
            // forma.Norte.CalcularPosicionDesde(forma, nuevoPunto);
            // Pero en C# no es común ese auto-llamado circular
            return nuevoPunto;
        }

        // --- Caminar en dirección norte ---
        public override void Caminar(Entidades.Bicho unBicho)
        {
            var pos = unBicho.Posicion;
            pos?.IrAlNorte(unBicho);
        }

        // --- Obtener el elemento en dirección norte ---
        public override ElementoMapa ObtenerElementoEn(Contenedor contenedor, Forma forma)
        {
            var puntoDestino = CalcularPosicionDesde(forma);
            return contenedor.ObtenerElementoEnPosicion(puntoDestino);
        }

        // --- Poner un elemento en dirección norte ---
        public override void PonerElementoEn(Contenedor contenedor, ElementoMapa elemento, Forma forma)
        {
            var puntoDestino = CalcularPosicionDesde(forma);
            contenedor.PonerElementoEnPosicion(elemento, puntoDestino);
        }

        // --- Recorrer en dirección norte ---
        public override void Recorrer(Contenedor contenedor, Forma forma, Action<ElementoMapa> accion)
        {
            var elem = ObtenerElementoEn(contenedor, forma);
            if (elem != null)
                accion(elem);
        }
    }
}
