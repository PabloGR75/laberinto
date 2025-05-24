namespace Laberinto.Core.Models
{
    /// Interfaz común para las orientaciones (norte, sur, este, oeste, etc).
    public abstract class Orientacion
    {
        // Devuelve el nuevo punto al aplicar la orientación sobre una forma
        public abstract Punto CalcularPosicionDesde(Forma forma);

        // Caminar (por defecto, puedes hacer que devuelva la posición calculada o dejarlo abstracto)
        public abstract void Caminar(Entidades.Bicho unBicho);

        // Devuelve el elemento del contenedor en esta orientación
        public abstract ElementoMapa ObtenerElementoEn(Contenedor contenedor, Forma forma);

        // Asigna un elemento a la posición de esta orientación en el contenedor
        public abstract void PonerElementoEn(Contenedor contenedor, ElementoMapa elemento, Forma forma);

        // Recorre el elemento en la posición de esta orientación en el contenedor
        public abstract void Recorrer(Contenedor contenedor, Forma forma, Action<ElementoMapa> accion);

        public override string ToString() => GetType().Name;
    }
}