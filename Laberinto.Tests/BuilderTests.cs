using Xunit;
using System.Text.Json;
using System.Collections.Generic;
using Laberinto.Core;
using Laberinto.Core.Models;
using Laberinto.Core.Entidades;
using Laberinto.Core.Services;

namespace Laberinto.Tests
{
    public class BuilderTests  // Solo debe haber un modificador 'public' aquí
    {
        [Fact]
        public void Builder_FabricaPocima_DesdeJSON()  // Los métodos de test deben ser public
        {
            // Arrange
            var director = new Director();
            
            var json = @"{
                ""forma"": ""cuadrado"",
                ""laberinto"": [
                    {
                        ""tipo"": ""habitacion"",
                        ""num"": 1,
                        ""hijos"": [
                            { ""tipo"": ""pocima"", ""valor"": 3 }
                        ]
                    }
                ]
            }";

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json, options);

            // Act
            director.Procesar(dict);
            var juego = director.ObtenerJuego() as JuegoLaberinto;
            
            var habitacion = juego?.Laberinto?.ObtenerHabitacion(1);
            var pocima = habitacion?.Hijos?.OfType<Pocima>()?.FirstOrDefault();

            // Assert
            Assert.NotNull(pocima);
            Assert.Equal(3, pocima?.ValorCuracion);
        }
    }
}