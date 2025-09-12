    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private long telefono;
    //private List<Pedido> ListadoPedidos;
    //private static int paga = 500;

        public int Id { get => id; }
    //public List<Pedido> ListadoPedidos1 { get => ListadoPedidos; }
        public string Nombre { get => nombre; }

        public Cadete(int id, string nombre, string direccion, long telefono)
        {
            this.id = id;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        //this.ListadoPedidos = new List<Pedido>();
        }



    }      


