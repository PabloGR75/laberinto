// Habitacion.cs
using System;
using Laberinto.Core.Services;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    /// <summary>
    /// Representa una habitación del laberinto, con cuatro lados.
    /// </summary>
    public class Habitacion : Contenedor
    {
        /// <summary>
        /// Identificador numérico de la habitación.
        /// </summary>
        public int Num { get; private set; }

        /// <summary>
        /// Crea una nueva habitación con el número dado. (Smalltalk initialize y num:) citeturn4file0turn4file3
        /// </summary>
        /// <param name="num">Número de la habitación.</param>
        public Habitacion(int num)
        {
            Num = num;
        }

        /// <summary>
        /// Indica que este elemento es una habitación. (Smalltalk esHabitacion) citeturn4file0
        /// </summary>
        public override bool EsHabitacion => true;

        /// <summary>
        /// Acepta un visitante específico para habitaciones. (Smalltalk visitarContenedor:) citeturn4file0
        /// </summary>
        /// <param name="visitor">Visitor para procesar la habitación.</param>
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitHabitacion(this);
        }

        /// <summary>
        /// Convierte la habitación a una representación de texto. (Smalltalk printOn:) citeturn4file0
        /// </summary>
        /// <returns>Cadena "Hab" seguida del número.</returns>
        public override string ToString()
        {
            return $"Hab{Num}";
        }

        /// <summary>
        /// Al entrar un ente en la habitación, propaga la llamada a los hijos. (Inherited)
        /// </summary>
        /// <param name="quien">Entidad que entra.</param>
        public override void Entrar(Ente quien)
        {
            base.Entrar(quien);
        }

        // Diccionario de puertas por orientación
        private readonly Dictionary<Orientacion, Puerta> puertas = new();

        /// <summary>
        /// Asocia una puerta a una orientación de la habitación.
        /// </summary>
        public void AgregarPuerta(Orientacion orientacion, Puerta puerta)
        {
            puertas[orientacion] = puerta;
        }

        /// <summary>
        /// Devuelve la habitación vecina en una orientación dada, si hay puerta y está abierta.
        /// </summary>
        public Habitacion ObtenerVecina(Orientacion orientacion)
        {
            if (puertas.TryGetValue(orientacion, out var puerta) && puerta.EstaAbierta)
            {
                // La puerta conecta dos habitaciones; devuelve la otra
                return puerta.OtroLado(this);
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
    }
}
