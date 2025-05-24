using System;
using System.Collections.Generic;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public abstract class Forma
    {
        protected List<Orientacion> orientaciones;
        public Punto Punto { get; set; }
        public object Extent { get; set; }
        public int Num { get; set; }
        public Contenedor Contenedor { get; set; }

        protected Forma()
        {
            orientaciones = new List<Orientacion>();
        }

        public virtual void AgregarOrientacion(Orientacion unaOr)
        {
            orientaciones.Add(unaOr);
        }

        public virtual void CalcularPosicion()
        {
            foreach (var or in orientaciones)
            {
                or.CalcularPosicionDesde(this);
            }
        }

        public object GetExtent()
        {
            return Extent;
        }

        public void SetExtent(object valor)
        {
            Extent = valor;
        }

        public int GetNum()
        {
            return Num;
        }

        public void SetNum(int valor)
        {
            Num = valor;
        }

        public List<Orientacion> ObtenerOrientaciones()
        {
            return orientaciones;
        }

        public virtual Orientacion ObtenerOrientacion()
        {
            if (orientaciones.Count == 0) return null;
            var rand = new Random();
            int ind = rand.Next(0, orientaciones.Count);
            return orientaciones[ind];
        }

        public List<Orientacion> GetOrientaciones()
        {
            return orientaciones;
        }

        public void SetOrientaciones(List<Orientacion> lista)
        {
            orientaciones = lista;
        }

        public virtual ElementoMapa ObtenerElementoOr(Orientacion unaOr)
        {
            if (Contenedor == null)
                throw new InvalidOperationException("Forma no tiene un contenedor asignado.");
            return unaOr.ObtenerElementoEn(Contenedor, this);
        }

        public virtual void PonerEnOr(Orientacion unaOr, ElementoMapa unEM)
        {
            if (Contenedor == null)
                throw new InvalidOperationException("Forma no tiene un contenedor asignado.");
            unaOr.PonerElementoEn(Contenedor, unEM, this);
        }

        // En Smalltalk: irAlNorte:alguien es responsabilidad de subclases
        public virtual void IrAlNorte(Ente alguien)
        {
            throw new NotImplementedException();
        }
        public virtual void IrAlSur(Ente alguien) { }
        public virtual void IrAlEste(Ente alguien) { }
        public virtual void IrAlOeste(Ente alguien) { }
        public virtual void IrAlNoreste(Ente alguien) { }
        public virtual void IrAlNoroeste(Ente alguien) { }
        public virtual void IrAlSureste(Ente alguien) { }
        public virtual void IrAlSuroeste(Ente alguien) { }
    }
}
