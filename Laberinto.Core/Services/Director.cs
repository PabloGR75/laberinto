
using System.Text.Json;

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
            Console.WriteLine($"[Director] JSON leído, claves: {string.Join(", ", Dict.Keys)}");

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
            Console.WriteLine("[Director] Fabricando laberinto...");
            Builder.FabricarLaberinto();

            if (Dict.TryGetValue("laberinto", out var labListObj))
            {
                if (labListObj is JsonElement labElem && labElem.ValueKind == JsonValueKind.Array)
                {
                    var habitaciones = labElem.EnumerateArray()
                        .Select(elem => JsonSerializer.Deserialize<Dictionary<string, object>>(elem.GetRawText()))
                        .ToList();
                    //Console.WriteLine($"[Director] Habitaciones encontradas: {habitaciones.Count}");
                    foreach (var each in habitaciones)
                    {
                        FabricarLaberintoRecursivo(each, "root");
                    }
                }
                else
                {
                    Console.WriteLine("[Director] 'laberinto' no es un array JsonElement.");
                }
            }
            else
            {
                Console.WriteLine("[Director] No se encontró la clave 'laberinto' en el JSON.");
            }

            if (Dict.TryGetValue("puertas", out var puertasListObj))
            {
                if (puertasListObj is JsonElement puertasElem && puertasElem.ValueKind == JsonValueKind.Array)
                {
                    foreach (var puertaArrElem in puertasElem.EnumerateArray())
                    {
                        if (puertaArrElem.ValueKind == JsonValueKind.Array)
                        {
                            var puertaItems = puertaArrElem.EnumerateArray().ToArray();
                            int numHabA = puertaItems[0].GetInt32();
                            string orA = puertaItems[1].GetString();
                            int numHabB = puertaItems[2].GetInt32();
                            string orB = puertaItems[3].GetString();

                            Builder.FabricarPuertaL1(numHabA, orA, numHabB, orB);
                            //Console.WriteLine($"[DEBUG] Puerta: {numHabA} ({orA}) <-> {numHabB} ({orB})");
                        }
                    }
                }
            }
        }

        public void FabricarLaberintoRecursivo(Dictionary<string, object> unDic, object padre)
        {
            if (!unDic.ContainsKey("tipo")) return;

            string tipo = unDic["tipo"].ToString().ToLower();
            object con = null;

            // Extracción segura del número
            int num = 0;
            if (unDic.ContainsKey("num"))
            {
                var numElement = unDic["num"];
                if (numElement is JsonElement je)
                {
                    if (je.ValueKind == JsonValueKind.Number)
                        num = je.GetInt32();
                    else if (je.ValueKind == JsonValueKind.String)
                        num = int.Parse(je.GetString());
                    else
                        throw new InvalidOperationException("El campo 'num' no es ni número ni string.");
                }
                else if (numElement is int n)
                {
                    num = n;
                }
                else if (numElement is string s)
                {
                    num = int.Parse(s);
                }
            }

            if (tipo == "habitacion")
                con = Builder.FabricarHabitacion(num);
            else if (tipo == "armario")
                con = Builder.FabricarArmario(num, padre);
            else if (tipo == "bomba")
                Builder.FabricarBombaEn(padre as Contenedor);
            else if (tipo == "tunel")
                Builder.FabricarTunelEn(padre as Contenedor);

            // Procesar hijos recursivamente (si existen)
            if (unDic.ContainsKey("hijos"))
            {
                var hijosElement = unDic["hijos"];
                if (hijosElement is JsonElement hijosJe && hijosJe.ValueKind == JsonValueKind.Array)
                {
                    foreach (var hijo in hijosJe.EnumerateArray())
                    {
                        // Cada hijo es un objeto tipo Dictionary<string, object>
                        var hijoDic = new Dictionary<string, object>();
                        foreach (var prop in hijo.EnumerateObject())
                            hijoDic[prop.Name] = prop.Value;

                        FabricarLaberintoRecursivo(hijoDic, con ?? padre);
                    }
                }
                // Si ya tienes una lista de diccionarios en vez de un JsonElement, recórrela igual
                else if (hijosElement is List<Dictionary<string, object>> hijosList)
                {
                    foreach (var hijoDic in hijosList)
                    {
                        FabricarLaberintoRecursivo(hijoDic, con ?? padre);
                    }
                }
            }
        }

        public void FabricarBichos()
        {
            if (Dict.TryGetValue("bichos", out var bichosListObj))
            {
                if (bichosListObj is JsonElement bichosElem && bichosElem.ValueKind == JsonValueKind.Array)
                {
                    foreach (var elem in bichosElem.EnumerateArray())
                    {
                        var modo = elem.GetProperty("modo").GetString();
                        var posicion = elem.GetProperty("posicion").GetInt32();
                        Builder.FabricarBichoModo(modo, posicion);
                    }
                }
                else if (bichosListObj is IEnumerable<object> bichosList)
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
        }

        public object ObtenerJuego()
        {
            return Builder.ObtenerJuego();
        }
    }
}
