// Habitacion.cs
using System;
using Laberinto.Core.Services;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    /// Representa una habitación del laberinto, con cuatro lados.
    public class Habitacion : Contenedor
    {
        /// Identificador numérico de la habitación.
        public int Num { get; private set; }

        /// Crea una nueva habitación con el número dado. (Smalltalk initialize y num:) citeturn4file0turn4file3
        /// <param name="num">Número de la habitación.</param>
        public Habitacion(int num)
        {
            Num = num;
        }

        /// Indica que este elemento es una habitación. (Smalltalk esHabitacion) citeturn4file0
        public override bool EsHabitacion => true;


        public override void VisitarContenedor(IVisitor visitor)
        {
            visitor.VisitHabitacion(this);
        }

        /// Acepta un visitante específico para habitaciones. (Smalltalk visitarContenedor:) citeturn4file0
        /// <param name="visitor">Visitor para procesar la habitación.</param>
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitHabitacion(this);
        }

        /// Convierte la habitación a una representación de texto. (Smalltalk printOn:) citeturn4file0
        /// <returns>Cadena "Hab" seguida del número.</returns>
        public override string ToString()
        {
            return $"Hab{Num}";
        }

        /// Al entrar un ente en la habitación, propaga la llamada a los hijos. (Inherited)
        /// <param name="quien">Entidad que entra.</param>
        public override void Entrar(Ente quien)
        {
            base.Entrar(quien);
        }

        // Diccionario de puertas por orientación
        private readonly Dictionary<Orientacion, Puerta> puertas = new();

        /// Asocia una puerta a una orientación de la habitación.
        public void AgregarPuerta(Orientacion orientacion, Puerta puerta)
        {
            puertas[orientacion] = puerta;
        }

        /// Devuelve la habitación vecina en una orientación dada, si hay puerta y está abierta.
        public Habitacion ObtenerVecina(Orientacion orientacion)
        {
            if (puertas.TryGetValue(orientacion, out var puerta) && puerta.EstaAbierta())
            {
                // La puerta conecta dos habitaciones; devuelve la otra
                return puerta.OtroLado(this) as Habitacion;
            }
            return null;
        }

        public Tunel BuscarTunel()
        {
            // Recorre los hijos de la habitación y devuelve el primer túnel que encuentre
            foreach (var hijo in Hijos)
            {
                if (hijo is Tunel tunel)
                    return tunel;
            }
            return null;
        }

        public virtual Orientacion ObtenerOrientacion()
        {
            // Lógica para devolver la orientación relevante.
            // Ejemplo simple: devuelve una por defecto, o según contexto/juego.
            return null;
        }

        public virtual void IrAlNorte(Ente ente)
        {
            var vecina = ObtenerVecina(Norte.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

        public virtual void IrAlSur(Ente ente)
        {
            var vecina = ObtenerVecina(Sur.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

        public virtual void IrAlEste(Ente ente)
        {
            var vecina = ObtenerVecina(Este.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

        public virtual void IrAlOeste(Ente ente)
        {
            var vecina = ObtenerVecina(Oeste.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

        public virtual void IrAlNoreste(Ente ente)
        {
            var vecina = ObtenerVecina(Noreste.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

        public virtual void IrAlNoroeste(Ente ente)
        {
            var vecina = ObtenerVecina(Noroeste.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

        public virtual void IrAlSureste(Ente ente)
        {
            var vecina = ObtenerVecina(Sureste.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

        public virtual void IrAlSuroeste(Ente ente)
        {
            var vecina = ObtenerVecina(Suroeste.Instancia);
            if (vecina != null)
            {
                ente.EntrarEn(vecina);
            }
        }

    }
}
