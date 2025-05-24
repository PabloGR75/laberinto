using System;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public class Puerta : ElementoMapa
    {
        public Contenedor Lado1 { get; set; }
        public Contenedor Lado2 { get; set; }
        public EstadoPuerta Estado { get; set; }
        public bool Visitada { get; set; }

        public Puerta()
        {
            Estado = new Cerrada();
            Visitada = false;
        }

        public Puerta(Contenedor lado1, Contenedor lado2)
            : this()
        {
            Lado1 = lado1;
            Lado2 = lado2;
        }
        
        public override void Entrar(Ente alguien)
        {
            Estado.Entrar(alguien, this);
        }

        public void Abrir()
        {
            Estado.Abrir(this);
        }

        public void Cerrar()
        {
            Estado.Cerrar(this);
        }

        public bool EstaAbierta()
        {
            return Estado.EstaAbierta();
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitPuerta(this);
        }

        public override bool EsPuerta => true;

        // Permite que un ente cruce la puerta al otro lado
        public void PuedeEntrar(Ente alguien)
        {
            // Si el ente está en lado1, lo mandamos al lado2, si no, al lado1
            if (alguien.Posicion == Lado1)
            {
                Lado2.Entrar(alguien);
            }
            else
            {
                Lado1.Entrar(alguien);
            }
        }

        public Contenedor OtroLado(Contenedor contenedor)
        {
            if (Lado1 == contenedor) return Lado2;
            if (Lado2 == contenedor) return Lado1;
            return null;
        }

        public void CalcularPosicionDesde(Forma unaForma, Punto unPunto)
        {
            if (Visitada) return;
            Visitada = true;
            if (unaForma.Num == (Lado1 as Contenedor)?.Num)
            {
                Lado2.SetPunto(unPunto);
                Lado2.CalcularPosicion();
            }
            else
            {
                Lado1.SetPunto(unPunto);
                Lado1.CalcularPosicion();
            }
        }

        public Contenedor GetLado1() => Lado1;
        public void SetLado1(Contenedor value) => Lado1 = value;

        public Contenedor GetLado2() => Lado2;
        public void SetLado2(Contenedor value) => Lado2 = value;

        public override string ToString()
        {
            return $"Puerta-{Lado1?.Num}-{Lado2?.Num}";
        }
    }
}
