using Xunit;

using Laberinto.Core;
using Laberinto.Core.Models;
using Laberinto.Core.Entidades;
using Laberinto.Core.Services;

namespace Laberinto.Tests
{
    public class ComandoTests
    {
        [Fact]
        public void ComandoTomarPocima_EjecutaCorrectamente()
        {
            // Arrange
            var pocima = new Pocima(1);
            var comando = new ComandoTomarPocima(pocima);
            var personaje = new Personaje("Test");
            int vidasIniciales = personaje.Vidas;

            // Act
            comando.Ejecutar(personaje);

            // Assert
            Assert.Equal(vidasIniciales + 1, personaje.Vidas);
            Assert.True(pocima.Consumida);
        }
    }
}