using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Pocima : ElementoMapa
    {
        public int ValorCuracion { get; set; }
        public bool Consumida { get; set; }

        public Pocima(int valorCuracion = 1)
        {
            ValorCuracion = valorCuracion;
            Consumida = false;
            ComandosLista.Add(new ComandoTomarPocima(this));
        }

        public string TomarPocima(Personaje personaje)
        {
            if (Consumida)
                return "Esta pócima ya fue consumida";

            personaje.Vidas += ValorCuracion;
            Consumida = true;
            return $"{personaje.Nombre} recuperó {ValorCuracion} vidas. Vidas totales: {personaje.Vidas}";
        }

        // Implementación requerida de Entrar
        public override void Entrar(Ente quien)
        {
            // Vacío intencionalmente - no queremos que se consuma automáticamente
            // Solo se consumirá cuando se ejecute explícitamente el comando
        }

        public override bool EsPocima => true;

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitPocima(this);
        }

        public override string ToString()
        {
            return $"Pócima (+{ValorCuracion})";
        }
    }

    // Clase concreta para el comando
    public class ComandoTomarPocima : Comando
    {
        private readonly Pocima _pocima;

        public ComandoTomarPocima(Pocima pocima) : base(pocima)
        {
            _pocima = pocima;
        }

        public override void Ejecutar(Ente quien)
        {
            if (quien is Personaje personaje)
            {
                Console.WriteLine(_pocima.TomarPocima(personaje));
            }
        }

        public override string ToString()
        {
            return "Tomar pócima";
        }
    }
}