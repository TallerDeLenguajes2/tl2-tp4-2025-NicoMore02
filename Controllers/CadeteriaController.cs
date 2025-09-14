using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace tl2_tp4_2025_NicoMore02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;
    private AccesoADatosCadeteria ADCadeteria;
    private AccesoADatosCadetes ADCadetes;
    private AccesoADatosPedidos ADPedidos;

    public CadeteriaController()
    {
        ADCadeteria = new AccesoADatosCadeteria();
        ADCadetes = new AccesoADatosCadetes();
        ADPedidos = new AccesoADatosPedidos();

        cadeteria = ADCadeteria.Obtener();
        cadeteria.AnadirCadete(ADCadetes.Obtener());
        cadeteria.anadirpedido(ADPedidos.Obtener());
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
        AccesoADatosPedidos.Guardar(result);

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
        Pedido asignado = cadeteria.AsignarCadeteAPedido(idCadete, NroPedido);
        if (!asignado)
        {
            NotFound("Error al asignar");
        }
        AccesoADatosPedidos.Guardar(asignado);
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
        Pedido estado = cadeteria.CambiarEstado(Nro, opcion);

        if (estado)
        {
            return Ok("Exito al cambiar el estado del pedido");
            AccesoADatosPedidos.Guardar(estado);
        }
        else
        {
            return NotFound("Error al cambiar el estado del pedido");
        }
    }

    [HttpPut("CambiarCadetePedido/{idCadeteDestino}/{idCadeteOrigen}/{nroPedido}")]
    public IActionResult CambiarCadetePedido(int idCadeteOrigen, int nroPedido, int idCadeteDestino)
    {
        Pedido result = cadeteria.ReasignarPedido(idCadeteOrigen, nroPedido, idCadeteDestino);
        if (!result)
        {
            return NotFound("Error al cambiar el cadete");
        }
        AccesoADatosPedidos.Guardar(result);
        return Ok("Exito al cambiar el cadete");
    }
}
