using System;

namespace Laberinto.Core.Models
{
    // Estado de la puerta cerrada: impide el paso
    public class Cerrada : EstadoPuerta
    {
        public override void Abrir(Puerta unaPuerta)
        {
            //Console.WriteLine($"{unaPuerta} abierta");
            unaPuerta.Estado = new Abierta();
        }

        public override void Cerrar(Puerta unaPuerta)
        {
            // Ya está cerrada, no hace nada
        }

        public override void Entrar(Entidades.Ente alguien, Puerta unaPuerta)
        {
            Console.WriteLine($"{alguien} choca con {unaPuerta}");
        }

        public override bool EstaAbierta() => false;

        public override EstadoPuerta DeepClone() => new Cerrada();
    }
}
