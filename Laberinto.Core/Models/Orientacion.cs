namespace Laberinto.Core.Models
{
    public abstract class Orientacion
    {
        // Devuelve el nuevo punto al aplicar la orientación sobre una forma (posición + dirección)
        public abstract Punto CalcularPosicionDesde(Forma forma);

        // Camina (puedes hacer que sea igual que calcular posición desde, si es así en tu modelo)
        public virtual Punto Caminar(Forma forma)
        {
            return CalcularPosicionDesde(forma);
        }

        // Métodos abstractos a implementar según el diseño Smalltalk:
        public abstract ElementoMapa ObtenerElementoEn(Contenedor contenedor, Forma forma);

        public abstract void PonerElementoEn(Contenedor contenedor, ElementoMapa elemento, Forma forma);

        public abstract void Recorrer(Contenedor contenedor, Forma forma, Action<ElementoMapa> accion);

        public override string ToString() => GetType().Name;
    }
}

