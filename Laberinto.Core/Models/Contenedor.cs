using System;
using System.Collections.Generic;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public abstract class Contenedor : ElementoMapa
    {
        protected List<ElementoMapa> Hijos { get; } = new List<ElementoMapa>();

        public Forma Forma { get; set; }

        public Contenedor(Forma forma)
        {
            Forma = forma;
        }

        public Contenedor() { }

        public void AgregarHijo(ElementoMapa hijo)
        {
            if (hijo == null) throw new ArgumentNullException(nameof(hijo));
            Hijos.Add(hijo);
            hijo.Padre = this;
        }

        public void EliminarHijo(ElementoMapa hijo)
        {
            Hijos.Remove(hijo);
            if (hijo.Padre == this)
                hijo.Padre = null;
        }

        public override void Recorrer(Action<ElementoMapa> bloque)
        {
            bloque(this);
            foreach (var hijo in Hijos)
                hijo.Recorrer(bloque);
        }

        public override void Entrar(Ente quien)
        {
            foreach (var hijo in Hijos)
                hijo.Entrar(quien);
        }

        // Helpers para integración con Orientacion y Forma
        public virtual ElementoMapa ObtenerElementoEnPosicion(Punto punto)
        {
            foreach (var hijo in Hijos)
            {
                // Si tus elementos tienen posición, adáptalo aquí.
                if (hijo is IPosicionable pos && pos.Punto.Equals(punto))
                    return hijo;
            }
            return null;
        }

        public virtual void PonerElementoEnPosicion(ElementoMapa elemento, Punto punto)
        {
            var existente = ObtenerElementoEnPosicion(punto);
            if (existente != null)
                EliminarHijo(existente);

            if (elemento is IPosicionable pos)
            {
                pos.Punto = punto;
                AgregarHijo(elemento);
            }
            else
            {
                AgregarHijo(elemento);
            }
        }
    }
}
