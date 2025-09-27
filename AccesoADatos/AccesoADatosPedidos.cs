using System.Text.Json;

public class AccesoADatosPedidos
{
    private string ruta = Path.Combine("json", "Pedidos.json");
    public List<Pedido> Obtener()
    {
        if (!File.Exists(ruta))
        {
            return new List<Pedido>();
        }

        string json = File.ReadAllText(ruta);
        //var datos = JsonSerializer.Deserialize<List<Pedido>>(json);
        return JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
    }

    public void Guardar(List<Pedido> PedidoActualizados)
    {
        var PedidoExistentes = Obtener();
        foreach (var item in PedidoActualizados)
        {
            var PedidoExistente = PedidoExistentes.FirstOrDefault(p => p.Nro1 == item.Nro1);
            if (PedidoExistente != null)
            {
                PedidoExistentes.Remove(PedidoExistente);
            }
            PedidoExistentes.Add(PedidoExistente);
        }
        var json = JsonSerializer.Serialize(PedidoExistentes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ruta, json);
    }
}