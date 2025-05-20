namespace Laberinto.Core.Models
{
    // Clase base abstracta
    public abstract class Forma
    {
        public string Nombre { get; protected set; }
        public int Lados { get; protected set; }
        public List<Orientacion> Orientaciones { get; protected set; }
        public (int ancho, int alto) Extent { get; protected set; } // 'extent' en Smalltalk

        public Punto Punto { get; protected set; }
        public Orientacion OrientacionActual { get; protected set; }

        protected Forma(string nombre, int lados, IEnumerable<Orientacion> orientaciones, int ancho = 0, int alto = 0)
        {
            Nombre = nombre;
            Lados = lados;
            Orientaciones = new List<Orientacion>(orientaciones);
            Extent = (ancho, alto);
        }
        protected Forma(string nombre, int lados, IEnumerable<Orientacion> orientaciones, Punto punto, Orientacion orientacion, int ancho = 0, int alto = 0)
                    : this(nombre, lados, orientaciones, ancho, alto)
        {
            Punto = punto;
            OrientacionActual = orientacion;
        }

        protected Forma() : this("", 0, new List<Orientacion>(), 0, 0) { }

        // Método Smalltalk: agregarOrientacion:
        public void AgregarOrientacion(Orientacion orientacion)
        {
            if (!Orientaciones.Contains(orientacion))
                Orientaciones.Add(orientacion);
        }

        // Devuelve una nueva Forma movida en la orientación actual (avanzar)
        public abstract Forma Avanzar();

        // Devuelve una nueva Forma en la misma posición pero con nueva orientación
        public abstract Forma CambiarOrientacion(Orientacion nuevaOrientacion);

        // Devuelve el punto resultante de calcularPosicion desde esta forma en la orientación dada
        public abstract Punto CalcularPosicion(Orientacion orientacion);

        // Pone un elemento en la forma en la orientación dada (Smalltalk: ponerenor:elemento:)
        // (La implementación puede variar según cómo modeles el mapa)
        public abstract void PonerEnOr(Contenedor contenedor, Orientacion orientacion, ElementoMapa elemento);
    }
}
