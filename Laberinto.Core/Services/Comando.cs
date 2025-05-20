using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

public abstract class Comando
{
  protected Ente Receptor { get; }
  protected Comando(Ente receptor)
  {
    Receptor = receptor;
  }
  public abstract void Ejecutar();
}
