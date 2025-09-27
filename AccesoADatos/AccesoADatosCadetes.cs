using System.Text.Json;

public class AccesoADatosCadetes
{
    private string ruta = Path.Combine("json", "Cadetes.json");
    public List<Cadete> Obtener()
    {
        if (!File.Exists(ruta))
        {
            return new List<Cadete>();
        }
        string json = File.ReadAllText(ruta);
        return JsonSerializer.Deserialize<List<Cadete>>(json) ?? new List<Cadete>();
        //return datos;
    }
}