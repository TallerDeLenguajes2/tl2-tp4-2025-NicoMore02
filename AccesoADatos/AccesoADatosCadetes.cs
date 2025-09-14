public class AccesoADatosCadetes
{
    private string ruta = "Cadetes.json";
    public List<Cadetes> Obtener()
    {
        if (!File.Exists(ruta))
        {
            return new List<Cadetes>();
        }
        var json = File.ReadAllText(ruta);
        var datos = JsonSerializer.Deserialize<List<Cadete>>(json);
        return datos;
    }
}