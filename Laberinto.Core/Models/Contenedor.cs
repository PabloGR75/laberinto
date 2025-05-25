using System;
using System.Collections.Generic;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public abstract class Contenedor : ElementoMapa
    {
        protected List<ElementoMapa> hijos = new List<ElementoMapa>();
        public Forma Forma { get; set; }
        public int Num { get; set; }

        public Contenedor() { }

        public List<ElementoMapa> Hijos => hijos;

        public override ElementoMapa DeepClone()
        {
            var clone = (Contenedor)this.MemberwiseClone();
            clone.Comandos.Clear();
            foreach (var comando in this.Comandos)
            {
                clone.Comandos.Add(comando); // O comando.DeepClone()
            }
            // Resto del clonado...
            return clone;
        }

        public virtual void AgregarHijo(ElementoMapa unEM)
        {
            unEM.Padre = this;
            hijos.Add(unEM);
        }

        public virtual void EliminarHijo(ElementoMapa unEM)
        {
            hijos.Remove(unEM);
            if (unEM.Padre == this)
                unEM.Padre = null;
        }

        public virtual void AgregarOrientacion(Orientacion unaOr)
        {
            Forma?.AgregarOrientacion(unaOr);
        }

        public virtual void CalcularPosicion()
        {
            Forma?.CalcularPosicion();
        }

        // Permite que un ente entre en este contenedor.
        public override void Entrar(Ente alguien)
        {
            // Si es una habitación, se asigna la posición.
            if (this is Habitacion)
                alguien.Posicion = this as Habitacion;

            // Si tiene hijos, propaga la entrada (Composite).
            foreach (var hijo in hijos)
                hijo.Entrar(alguien);

            // Busca túnel si corresponde.
            alguien.BuscarTunel();
        }

        public override void Recorrer(Action<ElementoMapa> unBloque)
        {
            unBloque(this);
            foreach (var hijo in hijos)
                hijo.Recorrer(unBloque);

            // Recorre orientaciones si la forma existe.
            if (Forma != null)
            {
                foreach (var or in Forma.ObtenerOrientaciones())
                {
                    or.Recorrer(this, Forma, unBloque);
                }
            }
        }

        public virtual object GetExtent() => Forma?.Extent;
        public virtual void SetExtent(object valor) => Forma.Extent = valor;

        public virtual Punto? GetPunto() => Forma?.Punto;
        public virtual void SetPunto(Punto p) => Forma.Punto = p;

        public virtual int GetNum() => Num;
        public virtual void SetNum(int valor) => Num = valor;

        public virtual ElementoMapa ObtenerElementoOr(Orientacion unaOr)
        {
            return Forma?.ObtenerElementoOr(unaOr);
        }

        public virtual Orientacion ObtenerOrientacion()
        {
            return Forma?.ObtenerOrientacion();
        }

        public virtual List<Orientacion> ObtenerOrientaciones()
        {
            return Forma?.ObtenerOrientaciones();
        }

        public virtual void PonerEnOr(Orientacion unaOr, ElementoMapa unEM)
        {
            Forma?.PonerEnOr(unaOr, unEM);
        }

        // Movimiento, delega a la forma
        public virtual void IrAlNorte(Ente alguien) => Forma?.IrAlNorte(alguien);
        public virtual void IrAlSur(Ente alguien) => Forma?.IrAlSur(alguien);
        public virtual void IrAlEste(Ente alguien) => Forma?.IrAlEste(alguien);
        public virtual void IrAlOeste(Ente alguien) => Forma?.IrAlOeste(alguien);
        public virtual void IrAlNoreste(Ente alguien) => Forma?.IrAlNoreste(alguien);
        public virtual void IrAlNoroeste(Ente alguien) => Forma?.IrAlNoroeste(alguien);
        public virtual void IrAlSureste(Ente alguien) => Forma?.IrAlSureste(alguien);
        public virtual void IrAlSuroeste(Ente alguien) => Forma?.IrAlSuroeste(alguien);

        // Visitor pattern (debe ser abstracto para obligar a las subclases a implementarlo)
        public abstract void VisitarContenedor(IVisitor visitor);

        public override void Accept(IVisitor visitor)
        {
            VisitarContenedor(visitor);
            foreach (var hijo in hijos)
                hijo.Accept(visitor);
            // Recorre orientaciones si procede
            if (Forma != null)
            {
                foreach (var or in Forma.ObtenerOrientaciones())
                {
                    or.Recorrer(this, Forma, elem => elem.Accept(visitor));
                }
            }
        }

        // Helpers para integración con Punto/Posicionable si lo necesitas (opcional, según tu diseño)
        public virtual ElementoMapa ObtenerElementoEnPosicion(Punto punto)
        {
            foreach (var hijo in hijos)
            {
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
