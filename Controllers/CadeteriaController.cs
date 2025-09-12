using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace tl2_tp4_2025_NicoMore02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria = new Cadeteria("Sangunesa", 381238792124);


    public CadeteriaController()
    {
        Cliente cliente1 = new Cliente("Fabricio", "Barrio Policial", 3818920471, "Porton negro");
        Cliente cliente2 = new Cliente("María", "Centro", 3812345678, "Casa esquina");
        cadeteria.CrearPedido("Milanesa", "De Carne", cliente1);
        cadeteria.CrearPedido("Pizza", "Napolitana", cliente2);
    }

    [HttpGet]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        if (cadeteria.Pedidos is null) NotFound();
        return Ok(cadeteria.Pedidos);
    }
}