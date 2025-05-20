// Punto.cs
namespace Laberinto.Core.Models
{
    public struct Punto
    {
        public int X { get; }
        public int Y { get; }

        public Punto(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Punto Mover(int dx, int dy) => new Punto(X + dx, Y + dy);

        public override string ToString() => $"({X},{Y})";
    }
}
