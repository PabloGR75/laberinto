
using Laberinto.Core.Entidades;
using Laberinto.Core.Models;

namespace Laberinto.Core.Services
{
    public class Director
    {
        public LaberintoBuilder Builder { get; set; }
        public Dictionary<string, object> Dict { get; set; }

        public Director() { }

        public void IniBuilder()
        {
            if (Dict.TryGetValue("forma", out var forma))
            {
                if (forma.ToString() == "cuadrado")
                    Builder = new LaberintoBuilder();
                else if (forma.ToString() == "rombo")
                    Builder = new LaberintoBuilderRombo();
                // Añade aquí más tipos de builder si amplías formas
            }
        }

        public void LeerArchivo(IDictionary<string, object> archivoJson)
        {
            // El parámetro sería el resultado de un JSON parseado (ya como Dictionary)
            Dict = new Dictionary<string, object>(archivoJson);
        }

        public void Procesar(IDictionary<string, object> archivoJson)
        {
            LeerArchivo(archivoJson);
            IniBuilder();
            FabricarLaberinto();
            FabricarJuego();
            FabricarBichos();
        }

        public void FabricarJuego()
        {
            Builder.FabricarJuego();
        }

        public void FabricarLaberinto()
        {
            Builder.FabricarLaberinto();

            if (Dict.TryGetValue("laberinto", out var labListObj) && labListObj is IEnumerable<object> labList)
            {
                foreach (var each in labList)
                {
                    if (each is Dictionary<string, object> subDict)
                        FabricarLaberintoRecursivo(subDict, "root");
                }
            }

            if (Dict.TryGetValue("puertas", out var puertasListObj) && puertasListObj is IEnumerable<object> puertasList)
            {
                foreach (var each in puertasList)
                {
                    if (each is List<object> puertaParams && puertaParams.Count == 4)
                    {
                        Builder.FabricarPuertaL1(
                            Convert.ToInt32(puertaParams[0]),
                            puertaParams[1].ToString(),
                            Convert.ToInt32(puertaParams[2]),
                            puertaParams[3].ToString());
                    }
                }
            }
        }

        public void FabricarLaberintoRecursivo(Dictionary<string, object> unDic, object padre)
        {
            object con = null;
            var tipo = unDic["tipo"].ToString();

            if (tipo == "habitacion")
                con = Builder.FabricarHabitacion(Convert.ToInt32(unDic["num"]));
            else if (tipo == "armario")
                con = Builder.FabricarArmario(Convert.ToInt32(unDic["num"]), padre);
            else if (tipo == "bomba" && padre is Contenedor contenedorBomba)
                Builder.FabricarBombaEn(contenedorBomba);
            else if (tipo == "tunel" && padre is Contenedor contenedorTunel)
                Builder.FabricarTunelEn(contenedorTunel);

            if (unDic.TryGetValue("hijos", out var hijosObj) && hijosObj is IEnumerable<object> hijos)
            {
                foreach (var each in hijos)
                {
                    if (each is Dictionary<string, object> subHijo)
                        FabricarLaberintoRecursivo(subHijo, con);
                }
            }
        }

        public void FabricarBichos()
        {
            if (Dict.TryGetValue("bichos", out var bichosListObj) && bichosListObj is IEnumerable<object> bichosList)
            {
                foreach (var each in bichosList)
                {
                    if (each is Dictionary<string, object> bichoDict)
                    {
                        var modo = bichoDict["modo"].ToString();
                        var posicion = Convert.ToInt32(bichoDict["posicion"]);
                        Builder.FabricarBichoModo(modo, posicion);
                    }
                }
            }
        }

        public object ObtenerJuego()
        {
            return Builder.ObtenerJuego();
        }
    }
}
