using Xunit;
using Laberinto.Core.Models;
using Laberinto.Core.Entidades;

namespace Laberinto.Tests.Models
{
    public class PocimaTests
    {
        [Fact]
        public void Pocima_SeCreaCorrectamente()
        {
            // Arrange
            int valorCuracion = 3;

            // Act
            var pocima = new Pocima(valorCuracion);

            // Assert
            Assert.Equal(valorCuracion, pocima.ValorCuracion);
            Assert.False(pocima.Consumida);
            Assert.True(pocima.EsPocima);
        }

        [Fact]
        public void Pocima_TomarPocima_CuraCorrectamente()
        {
            // Arrange
            var pocima = new Pocima(2);
            var personaje = new Personaje("Test");
            int vidasIniciales = personaje.Vidas;

            // Act
            string resultado = pocima.TomarPocima(personaje);

            // Assert
            Assert.Equal(vidasIniciales + 2, personaje.Vidas);
            Assert.True(pocima.Consumida);
            Assert.Contains("recuperó 2 vidas", resultado);
        }

        [Fact]
        public void Pocima_TomarPocimaConsumida_NoTieneEfecto()
        {
            // Arrange
            var pocima = new Pocima(1);
            var personaje = new Personaje("Test");
            pocima.TomarPocima(personaje); // Primera vez (la consume)
            int vidasDespuesPrimerUso = personaje.Vidas;

            // Act
            string resultado = pocima.TomarPocima(personaje); // Segundo intento

            // Assert
            Assert.Equal(vidasDespuesPrimerUso, personaje.Vidas);
            Assert.Contains("ya fue consumida", resultado);
        }
    }
}