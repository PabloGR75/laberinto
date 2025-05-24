using Laberinto.Core.Entidades;

namespace Laberinto.Core.Models
{
  /// Interfaz común para los comandos del laberinto (Command pattern).
  public abstract class Comando
  {
    // Receptor sobre el que se ejecuta el comando.
    public ElementoMapa Receptor { get; set; }

    protected Comando(ElementoMapa receptor)
    {
      Receptor = receptor;
    }

    // Método de ejecución polimórfico
    public abstract void Ejecutar(Ente quien);
  }
}
