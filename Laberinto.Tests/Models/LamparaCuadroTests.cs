using Xunit;
using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

namespace Laberinto.Tests.Models
{
    public class LamparaCuadroTests
    {
        [Fact]
        public void Lampara_SeCreaCorrectamente()
        {
            // Arrange & Act
            var lampara = new Lampara();

            // Assert
            Assert.True(lampara.EsLampara);
            Assert.False(lampara.EsCuadro);
            Assert.Equal("Lámpara", lampara.ToString());
        }

        [Fact]
        public void Cuadro_SeCreaConDescripcionCorrecta()
        {
            // Arrange
            string descripcionTest = "Retrato de un héroe";

            // Act
            var cuadro = new Cuadro { Descripcion = descripcionTest };

            // Assert
            Assert.True(cuadro.EsCuadro);
            Assert.False(cuadro.EsLampara);
            Assert.Contains(descripcionTest, cuadro.ToString());
        }
    }
}