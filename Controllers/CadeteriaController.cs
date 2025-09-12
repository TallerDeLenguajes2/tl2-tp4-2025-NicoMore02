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
        cadeteria.AnadirCadete(1, "Carlos", "Calle Falsa 123", 11223344);
        cadeteria.AnadirCadete(2, "Lucía", "Av. Libertad 456", 11998877);
        Cliente cliente1 = new Cliente("Fabricio", "Barrio Policial", 3818920471, "Porton negro");
        Cliente cliente2 = new Cliente("María", "Centro", 3812345678, "Casa esquina");
        cadeteria.CrearPedido("Milanesa", "De Carne", cliente1);
        cadeteria.CrearPedido("Pizza", "Napolitana", cliente2);
    }

    /// <summary>
    /// Se obtiene la lista de los pedidos
    /// </summary>
    /// <returns>Devuelve la lista de pedidos</returns>
    [HttpGet("pedidos")]
    public IActionResult GetPedidos()
    {
        if (cadeteria.Pedidos is null) NotFound();
        return Ok(cadeteria.Pedidos);
    }


    /// <summary>
    /// Se obtiene la lista de todos los cadetes disponibles
    /// </summary>
    /// <returns>lista de cadetes</returns>
    [HttpGet("cadetes")]
    public IActionResult GetCadetes()
    {
        if (cadeteria.Cadetes1 is null) NotFound();
        return Ok(cadeteria.Cadetes1);
    }

}