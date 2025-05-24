using System;
using System.Collections.Generic;
using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
    public abstract class ElementoMapa
    {
        public ElementoMapa Padre { get; set; }
        protected List<Comando> Comandos { get; } = new List<Comando>();

        public virtual void Initialize()
        {
            Padre = null;
            Comandos.Clear();
        }

        public virtual void AgregarComando(Comando comando)
        {
            if (comando == null) throw new ArgumentNullException(nameof(comando));
            Comandos.Add(comando);
        }

        public virtual void EliminarComando(Comando comando)
        {
            if (comando == null) return;
            Comandos.Remove(comando);
        }

        public virtual List<Comando> ObtenerComandos()
        {
            return new List<Comando>(Comandos);
        }

        // Acceso directo a la lista (cuidado con la modificación externa)
        public List<Comando> ComandosLista => Comandos;
        
        public virtual void EliminarPosicionDesde(ElementoMapa posiblePadre)
        {
            if (posiblePadre is Contenedor contenedor)
            {
                contenedor.EliminarHijo(this);
            }
            Padre = null;
        }

        public virtual void CalcularPosicionDesde(Forma unaForma, Punto unPunto)
        {
            // No hace nada por defecto (igual que Smalltalk)
        }

        public virtual bool EsLaberinto => false;
        public virtual bool EsHabitacion => false;
        public virtual bool EsArmario => false;
        public virtual bool EsPared => false;
        public virtual bool EsPuerta => false;
        public virtual bool EsTunel => false;
        public virtual bool EsDecorador => false;
        public virtual bool EsBomba => false;
        public virtual bool EsTesoro => false;

        public virtual void Recorrer(Action<ElementoMapa> bloque)
        {
            bloque(this);
        }

        public abstract void Entrar(Ente quien);
        public abstract void Accept(IVisitor visitor);
    }
}
