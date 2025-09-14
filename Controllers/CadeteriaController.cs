using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace tl2_tp4_2025_NicoMore02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController : ControllerBase
{
    private static Cadeteria cadeteria = new Cadeteria("Sangunesa", 381238792124);


    public CadeteriaController()
    {
        if (!cadeteria.Cadetes1.Any())
        {

            cadeteria.AnadirCadete(1, "Carlos", "Calle Falsa 123", 11223344);
            cadeteria.AnadirCadete(2, "Lucía", "Av. Libertad 456", 11998877);
            cadeteria.AnadirCadete(3, "Lucía", "Av. Libertad 56", 119988772);
            Cliente cliente1 = new Cliente("Fabricio", "Barrio Policial", 3818920471, "Porton negro");
            Cliente cliente2 = new Cliente("María", "Centro", 3812345678, "Casa esquina");
            Cliente cliente3 = new Cliente("Oscar", "Centro", 38125345678, "Casa esquina");
            cadeteria.CrearPedido("Milanesa", "De Carne", cliente1);
            cadeteria.CrearPedido("Pizza", "Napolitana", cliente2);
            cadeteria.AsignarCadeteAPedido(1, 1);
            cadeteria.AsignarCadeteAPedido(2, 2);
        }
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

    /// <summary>
    /// Genera un informe 
    /// </summary>
    /// <returns>un informe</returns>
    [HttpGet("informe")]
    public IActionResult GetInforme()
    {
        var informe = new
        {
            TotalPedidos = cadeteria.PedidosTotal(),
            TotalCadetes = cadeteria.TotalDeCadetes(),
            TotalRecaudado = cadeteria.TotalRecaudado()
        };

        return Ok(informe);
    }


    /// <summary>
    /// Creacion del pedido del cliente
    /// </summary>
    /// <param name="pedido"></param>
    /// <returns>el pedido</returns>
    [HttpPost("AgregarPedido")]
    public IActionResult AgregarPedido([FromBody] Pedido pedido)
    {
        Cliente cliente = new Cliente(pedido.Cliente.Nombre, pedido.Cliente.Direccion, pedido.Cliente.Telefono, pedido.Cliente.Datosreferenciadireccion);

        var result = cadeteria.CrearPedido(pedido.Comida, pedido.Obs, cliente);

        return CreatedAtAction(nameof(GetPedidos), new { }, result);
    }


    /// <summary>
    /// Se asigna el pedido a un cadete
    /// </summary>
    /// <param name="idCadete"></param>
    /// <param name="NroPedido"></param>
    /// <returns>el estado de la asignacion</returns>
    [HttpPut("AsignarPedidos/{idCadete}/{NroPedido}")]
    public IActionResult AsignarPedido(int idCadete, int NroPedido)
    {
        bool asignado = cadeteria.AsignarCadeteAPedido(idCadete, NroPedido);
        if (asignado == false)
        {
            NotFound("Error al asignar");
        }

        return Ok("Pedido Asignado Correctamente");
    }

    /// <summary>
    /// Cambia el estado que se encuentra el pedido 
    /// </summary>
    /// <param name="Nro"></param>
    /// <param name="opcion"></param>
    /// <returns>si se realizo el cambio con exito o no</returns>
    [HttpPut("CambiarEstadodelpedido/{Nro}/{opcion}")]
    public IActionResult CambiarEstadoPedido(int Nro, int opcion)
    {
        bool estado = cadeteria.CambiarEstado(Nro, opcion);
        if (estado == true)
        {
            return Ok("Exito");
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("CambiarCadetePedido/{idCadeteDestino}/{idCadeteOrigen}/{nroPedido}")]
    public IActionResult CambiarCadetePedido(int idCadeteOrigen, int nroPedido, int idCadeteDestino)
    {
        bool result = cadeteria.ReasignarPedido(idCadeteOrigen, nroPedido, idCadeteDestino);
        if (result == false)
        {
            return NotFound("Error al cambiar el cadete");
        }
        return Ok("Exito al cambiar el cadete");
    }
}
