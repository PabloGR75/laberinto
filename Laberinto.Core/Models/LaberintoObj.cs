using System;
using System.Collections.Generic;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    // El laberinto, contenedor de habitaciones y demás elementos.
    public class LaberintoObj : Contenedor
    {
        public List<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();

        public override bool EsLaberinto => true;

        public override void VisitarContenedor(IVisitor visitor)
        {
            visitor.VisitLaberinto(this);
        }

        public LaberintoObj()
        {
            // Puede haber inicialización específica aquí si la necesitas.
        }

        // Añade una habitación al laberinto.
        public void AgregarHabitacion(Habitacion unaHabitacion)
        {
            Hijos.Add(unaHabitacion);
            Habitaciones.Add(unaHabitacion);
            unaHabitacion.Padre = this;
        }

        // Elimina una habitación.
        public void EliminarHabitacion(Habitacion unaHabitacion)
        {
            Hijos.Remove(unaHabitacion);
            Habitaciones.Remove(unaHabitacion);
            if (unaHabitacion.Padre == this)
                unaHabitacion.Padre = null;
        }

        public IEnumerable<Puerta> ObtenerTodasLasPuertas()
        {
            var set = new HashSet<Puerta>();
            if (Habitaciones == null)
            {
                Console.WriteLine("[DEBUG] Habitaciones es null en LaberintoObj.");
                yield break;
            }
            foreach (var hijo in Hijos)
            {
                if (hijo is Habitacion hab)
                {
                    if (hab.Puertas == null)
                    {
                        Console.WriteLine($"[DEBUG] Puertas es null en Habitación {hab.Num}.");
                        continue;
                    }

                    foreach (var puerta in hab.Puertas.Values)
                    {
                        if (puerta != null)
                            set.Add(puerta);
                    }
                }
            }
            foreach (var p in set)
                yield return p;
        }


        // Devuelve la habitación número 'num' (por número de habitación).
        public Habitacion ObtenerHabitacion(int num)
        {
            foreach (var hijo in Hijos)
            {
                if (hijo is Habitacion hab && hab.Num == num)
                    return hab;
            }
            return null;
        }

        // Hace que un ente entre en la habitación 1.
        public override void Entrar(Ente quien)
        {
            var hab1 = ObtenerHabitacion(1);
            hab1?.Entrar(quien);
        }

        // Recorre el laberinto aplicando un bloque de acción a cada elemento.
        public override void Recorrer(Action<ElementoMapa> bloque)
        {
            bloque(this);
            foreach (var hijo in Hijos)
                hijo.Recorrer(bloque);
            // NOTA: no recorre orientaciones porque en Smalltalk tampoco lo hace aquí.
        }

        // Visitor pattern
        public override void Accept(IVisitor visitor)
        {
            visitor.VisitLaberinto(this);
            // Si quieres recorrer los hijos también con el visitor aquí, descomenta:
            // foreach (var hijo in Hijos)
            //     hijo.Accept(visitor);
        }

        // Abrir todas las puertas del laberinto (patrón Command en Smalltalk).
        public void AbrirPuertas()
        {
            Recorrer(each =>
            {
                if (each.EsPuerta && each is Puerta puerta)
                    puerta.Abrir();
            });
        }

        // Cerrar todas las puertas del laberinto.
        public void CerrarPuertas()
        {
            Recorrer(each =>
            {
                if (each.EsPuerta && each is Puerta puerta)
                    puerta.Cerrar();
            });
        }

        // Impresión sencilla del laberinto.
        public override string ToString()
        {
            return "Laberinto";
        }
    }
}
