using Laberinto.Core.Models;

namespace Laberinto.Core.Entidades
{
    /// Estrategia base para los Bicho (Strategy Pattern).
    public abstract class Modo
    {
        // Template method: define el flujo principal
        public virtual void Actua(Bicho unBicho)
        {
            Dormir(unBicho);
            Caminar(unBicho);
            Atacar(unBicho);
        }

        // Hook methods para ser sobrescritos por subclases:
        public virtual void Dormir(Bicho unBicho)
        {
            // Por defecto, no hacer nada (o lanzar NotImplementedException si es obligatorio sobrescribir)
        }

        public virtual void Caminar(Bicho unBicho)
        {
            // Elegir una orientación aleatoria de la posición actual y caminar
            var or = unBicho.ObtenerOrientacion();
            // Caminar hacia esa orientación
            or?.Caminar(unBicho);
        }

        public virtual void Atacar(Bicho unBicho)
        {
            unBicho.Atacar();
        }

        // Consulta: ¿es de este tipo?
        public virtual bool EsAgresivo() => false;
        public virtual bool EsPerezoso() => false;

        // Búsqueda de túnel: override en subclases si quieres comportamiento especial
        public virtual Tunel BuscarTunelBicho(Bicho unBicho)
        {
            // Comportamiento predeterminado: no hacer nada
            return null;
        }
    }
}
