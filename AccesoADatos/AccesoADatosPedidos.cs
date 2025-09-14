public class AccesoADatosPedidos
{
    private string ruta = "Pedidos.json";
    public List<Pedido> Obtener()
    {
        if (!File.Exists(ruta))
        {
            return new List<Pedido>();
        }

        var json = File.ReadAllText(ruta);
        var datos = JsonSerializer.Deserialize<List<Pedido>>(json);
        return datos;
    }

    void Guardar(List<Pedido> Pedido)
    {
        var json = JsonSerializer.Serialize(Pedido, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ruta, json);
    }
}