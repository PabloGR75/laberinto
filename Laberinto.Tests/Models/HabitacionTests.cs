using Xunit;
using Laberinto.Core.Models;
using Laberinto.Core.Entidades;
using Laberinto.Core;

namespace Laberinto.Tests.Models
{
    public class HabitacionTests
    {
        [Fact]
        public void Habitacion_AgregaElementos_Correctamente()
        {
            // Arrange
            var habitacion = new Habitacion(1);
            var pocima = new Pocima();
            var lampara = new Lampara();
            var cuadro = new Cuadro();

            // Act
            habitacion.AgregarHijo(pocima);
            habitacion.AgregarHijo(lampara);
            habitacion.AgregarHijo(cuadro);

            // Assert
            Assert.Contains(pocima, habitacion.Hijos);
            Assert.Contains(lampara, habitacion.Hijos);
            Assert.Contains(cuadro, habitacion.Hijos);
            Assert.Equal(3, habitacion.Hijos.Count);
        }

        [Fact]
        public void Habitacion_DescribeElementos_Correctamente()
        {
            // Arrange
            var habitacion = new Habitacion(1);
            habitacion.AgregarHijo(new Pocima(2));
            habitacion.AgregarHijo(new Lampara());
            habitacion.AgregarHijo(new Cuadro { Descripcion = "Paisaje" });

            // Act
            string descripcion = habitacion.Describir(new JuegoLaberinto());

            // Assert
            Assert.Contains("Pócima (+2)", descripcion);
            Assert.Contains("lámpara", descripcion);
            Assert.Contains("Paisaje", descripcion);
        }
    }
}