using System.Text.Json;

public class AccesoADatosCadeteria
{
    private string ruta = Path.Combine("json", "DatosCadeteria");
    public Cadeteria Obtener()
    {
        if (!File.Exists(ruta))
        {
            return null;
        }
        string json = File.ReadAllText(ruta);
        //var datos = JsonSerializer.Deserialize<Cadeteria>(json);
        return JsonSerializer.Deserialize<Cadeteria>(json);
        //return datos;
    }
}