    public class Cadeteria
    {
        private int contadorPedidos = 0;
        private string nombre;
        private long telefono;
        private List<Cadete> Cadetes;
        private List<Pedido> pedidos;

        public List<Cadete> Cadetes1 { get => Cadetes; }
        public List<Pedido> Pedidos { get => pedidos; }

    public Cadeteria(string nombre, long telefono)
        {
            this.nombre = nombre;
            this.telefono = telefono;
            this.Cadetes = new List<Cadete>();
            this.pedidos = new List<Pedido>();
        }

        public bool AnadirCadete(int id, string nombre, string direccion, long telefono)
        {
            Cadete cadeteAnadir = new Cadete(id, nombre, direccion, telefono);
            Cadetes.Add(cadeteAnadir);
            return true;
        }

        public Pedido CrearPedido(string comida, string obs, Cliente cliente)
        {
            Pedido NuevoPedido = new Pedido(comida, obs, cliente);
            pedidos.Add(NuevoPedido);
            return NuevoPedido;
        }

    public void anadirpedido(Pedido pedido)
    {
        pedidos.Add(pedido);
    }
    public Pedido AsignarCadeteAPedido(int idCadete, int NroPedido)
    {
        foreach (var pedidos in Pedidos)
        {
            if (pedidos.Nro1 == NroPedido)
            {
                foreach (var cadetes in Cadetes)
                {
                    if (cadetes.Id == idCadete)
                    {
                        pedidos.Estado = Estados.EnCamino;
                        pedidos.CadeteAsignado(cadetes);
                        return pedidos;
                    }
                }
            }
        }
        return null;
    }

        public Cadete BuscarCadete(int id)
        {
            foreach (var item in Cadetes)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Pedido ReasignarPedido(int idCadeteOrigen, int nroPedido, int idCadeteDestino)
        {
            Cadete cadeteOrigen = null;
            Cadete cadeteDestino = null;
            Pedido pedidoEncontrado = null;


            foreach (var cadete in Cadetes)
            {
                if (cadete.Id == idCadeteOrigen)
                {
                    cadeteOrigen = cadete;
                    break;
                }
            }


            foreach (var cadete in Cadetes)
            {
                if (cadete.Id == idCadeteDestino)
                {
                    cadeteDestino = cadete;
                    break;
                }
            }


            if (cadeteOrigen == null)
            {

                return null;
            }
            if (cadeteDestino == null)
            {

                return null;
            }

            foreach (var pedido in Pedidos)
            {
                if (pedido.Nro1 == nroPedido)
                {
                    pedidoEncontrado = pedido;
                }
            }

            if (pedidoEncontrado == null)
            {
                return null;
            }


            EliminarPedido(nroPedido);
            Pedidos.Add(pedidoEncontrado);

            return pedidoEncontrado;
        }


        public int TotalDeCadetes()
        {
            return Cadetes.Count;
        }

        public int PedidosTotal()
        {
            return Pedidos.Count;
        }

        public string RecaudacionPorCadete()
        {
            foreach (var item in Cadetes)
            {
                for (int i = 0; i < Cadetes.Count; i++)
                {
                    if (item.Id == i)
                    {
                        int recau = JornalACobrar(i);
                        return $"Recaudado de {item.Nombre} es de {recau}";
                    }
                }
            }
            return "Error";
        }

        public int TotalRecaudado()
        {
            int total = 0;
            foreach (var cadetes in Cadetes)
            {
                for (int i = 0; i < Cadetes.Count; i++)
                {
                    if (cadetes.Id == i)
                    {
                        total += JornalACobrar(i);
                    }
                }
            }
            return total;
        }

        public int JornalACobrar(int idCadete)
        {
            int total = 0;
            foreach (var pedido in Pedidos)
            {
                if (pedido.Cadete.Id == idCadete)
                {
                    if (pedido.Estado == Estados.Entregado)
                    {
                        total += 500;
                    }

                }
            }
            return total;
        }

        public bool EliminarPedido(int Nro)
        {
            foreach (var pedido in Pedidos)
            {
                if (pedido.Nro1 == Nro)
                {
                    Pedidos.Remove(pedido);
                    return true;
                }
            }
            return false;
        }

        public Pedido CambiarEstado(int Nro, int numero)
        {
            switch (numero)
            {
                case 1:
                    foreach (var pedido in Pedidos)
                    {
                        if (pedido.Nro1 == Nro)
                        {
                            pedido.Estado = Estados.EnCamino;
                            return pedido;
                        }
                    }
                    break;
                case 2:
                    foreach (var pedido in Pedidos)
                    {
                        if (pedido.Nro1 == Nro)
                        {
                            pedido.Estado = Estados.Entregado;
                            return pedido;
                        }
                    }
                    break;
                case 3:
                    foreach (var pedido in Pedidos)
                    {
                        if (pedido.Nro1 == Nro)
                        {
                            pedido.Estado = Estados.Cancelado;
                            return pedido;
                        }
                    }
                    break;
            }
            return null;
        }

    }

