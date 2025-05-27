using System;

namespace Laberinto.Core.Models
{
    // Estado de la puerta abierta: permite el paso
    public class Abierta : EstadoPuerta
    {
        public override void Abrir(Puerta unaPuerta)
        {
            // Ya está abierta
            // (No se hace nada)
        }

        public override void Cerrar(Puerta unaPuerta)
        {
            Console.WriteLine($"{unaPuerta} cerrada");
            unaPuerta.Estado = new Cerrada();
        }

        public override void Entrar(Entidades.Ente alguien, Puerta unaPuerta)
        {
            Console.WriteLine($"{alguien} pasa por {unaPuerta}");
            unaPuerta.PuedeEntrar(alguien);
        }

        public override bool EstaAbierta() => true;

        public override EstadoPuerta DeepClone() => new Abierta();
    }
}
