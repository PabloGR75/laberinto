using Xunit;
using Laberinto.Core.Models;

namespace Laberinto.Tests
{
    public class HabitacionTests
    {
        [Fact]
        public void Habitacion_SeCreaConNumeroCorrecto()
        {
            // Arrange
            int numeroEsperado = 42;

            // Act
            var habitacion = new Habitacion(numeroEsperado);

            // Assert
            Assert.Equal(numeroEsperado, habitacion.Num);
            Assert.True(habitacion.EsHabitacion);
        }
    }
}
