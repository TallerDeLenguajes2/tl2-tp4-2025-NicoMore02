public class Pedido
{
    private static int contadorPedidos = 1;
    private int nro;
    private string comida;
    private string obs;
    private Cliente cliente;
    private Estados estado;
    private Cadete cadete;

    public int Nro1 { get => nro; }
    public Estados Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; }
    public string Comida { get => comida; }
    public Cliente Cliente { get => cliente; }

    public Pedido(string comida, string Obs, Cliente cliente)
    {
        this.nro = contadorPedidos++;
        this.comida = comida;
        this.obs = Obs;
        this.cliente = cliente;
        this.estado = Estados.Pendiente;
        this.cadete = null;
    }

    public string MostrarPedido()
    {
        return $"Numero de Orden: {nro} | Estado: {estado}";
    }

    public string MostrarDireccion()
    {
        return $"Direccion de la Entrega: {Cliente.Direccion}";
    }

    public string MostrarCliente()
    {
        return $"Nombre: {Cliente.Nombre} | Telefono: {Cliente.Telefono} | Referencia: {Cliente.Datosreferenciadireccion}";
    }

    public void CadeteAsignado(Cadete cadeteAsi)
    {
        this.cadete = cadeteAsi;
    }
}