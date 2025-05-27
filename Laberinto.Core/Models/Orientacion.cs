namespace Laberinto.Core.Models
{
    /// Interfaz común para las orientaciones (norte, sur, este, oeste, etc).
    public abstract class Orientacion
    {
        // Cada orientación concreta debe definir su nombre
        public abstract string Nombre { get; }

        // ToString por defecto devuelve el nombre de la clase
        public override string ToString()
        {
            return Nombre;
        }

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

        // Singleton factory: según el nombre, devuelve la única instancia
        public static Orientacion FromString(string nombre)
        {
            switch (nombre.Trim().ToLower())
            {
                case "norte": return Norte.Instancia;
                case "sur": return Sur.Instancia;
                case "este": return Este.Instancia;
                case "oeste": return Oeste.Instancia;
                case "noreste": return Noreste.Instancia;
                case "noroeste": return Noroeste.Instancia;
                case "sureste": return Sureste.Instancia;
                case "suroeste": return Suroeste.Instancia;
                default: throw new ArgumentException($"Orientación desconocida: {nombre}");
            }
        }

        // Igualdad por referencia, para que los diccionarios funcionen bien
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            // Un hashcode por referencia (rápido)
            return System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(this);
        }
    }
}