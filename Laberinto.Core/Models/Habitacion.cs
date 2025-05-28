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

        /// Crea una nueva habitación con el número dado. (Smalltalk initialize y num:)
        /// <param name="num">Número de la habitación.</param>
        public Habitacion(int num)
        {
            Num = num;
        }

        /// Indica que este elemento es una habitación. (Smalltalk esHabitacion) citeturn4file0
        public override bool EsHabitacion => true;

        public override ElementoMapa DeepClone()
        {
            var clone = (ElementoMapa)this.MemberwiseClone();

            // Clona la lista de comandos (nueva lista, mismos comandos - cambiar a deep si lo necesitas)
            clone.ComandosLista.Clear();
            foreach (var comando in this.ObtenerComandos())
            {
                clone.ComandosLista.Add(comando); // Si necesitas deep clone, comando.DeepClone()
            }

            // Clona otras propiedades si tenemos (como padre, etc.)
            // clone.Padre = ...;

            return clone;
        }

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

        public IReadOnlyDictionary<Orientacion, Puerta> Puertas => puertas;

        /// Asocia una puerta a una orientación de la habitación.
        public void AgregarPuerta(Orientacion orientacion, Puerta puerta)
        {
            if (!puertas.ContainsKey(orientacion))
                puertas.Add(orientacion, puerta);
            else
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

        public override string Describir(JuegoLaberinto juego)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine($"\n=== Estás en la Habitación {this.Num} ===");

            // PUERTAS
            sb.AppendLine("\nPuertas:");
            if (this.Puertas.Count > 0)
            {
                foreach (var kvp in this.Puertas)
                {
                    var puerta = kvp.Value;
                    var l1 = puerta.Lado1 as Habitacion;
                    var l2 = puerta.Lado2 as Habitacion;
                    string estado = puerta.EstaAbierta() ? "Abierta" : "Cerrada";
                    sb.AppendLine($"- Dirección {kvp.Key}: Conecta con Hab. {(l2?.Num == this.Num ? l1?.Num : l2?.Num)} - {estado}");
                }
            }
            else
            {
                sb.AppendLine("No hay puertas visibles.");
            }

            // ELEMENTOS
            sb.AppendLine("\nElementos:");

            // - Bombas
            var bombas = this.Hijos.OfType<Bomba>().Count();
            if (bombas > 0)
            {
                sb.AppendLine($"- ¡Hay {bombas} Bomba(s)! Ten cuidado");
            }
            else
            {
                sb.AppendLine("- No hay elementos especiales");
            }

            // BICHOS
            var bichos = this.ObtenerBichos(juego).ToList();
            if (bichos.Any())
            {
                sb.AppendLine("\nBichos:");
                foreach (var bicho in bichos)
                {
                    sb.AppendLine($"- {bicho.Modo?.GetType().Name} (Vidas: {bicho.Vidas}, Estado: {(bicho.EstaVivo() ? "Vivo" : "Muerto")})");
                }
            }

            return sb.ToString();
        }
        public IEnumerable<Bicho> ObtenerBichos(JuegoLaberinto juego)
        {
            return juego?.Bichos?.Where(b => b.Posicion == this) ?? Enumerable.Empty<Bicho>();
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
