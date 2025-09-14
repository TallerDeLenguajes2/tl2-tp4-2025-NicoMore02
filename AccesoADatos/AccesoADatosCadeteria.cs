public class AccesoADatosCadeteria
{
    private string ruta = "Cadeteria.json";
    public Cadeteria Obtener()
    {
        if (!File.Exists(ruta))
        {
            return null;
        }
        var json = File.ReadAllText(ruta);
        var datos = JsonSerializer.Deserialize<Cadeteria>(json);
        return datos;
    }
}