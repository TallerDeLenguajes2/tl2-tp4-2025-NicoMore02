        public class Cliente
    {
        private string nombre;
        private string direccion;
        private long telefono;
        private string datosreferenciadireccion;

        public Cliente(string nombre, string direccion, long telefono, string datosreferenciadireccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
            this.datosreferenciadireccion = datosreferenciadireccion;
        }

        public string Nombre { get => nombre; }
        public string Direccion { get => direccion; }
        public long Telefono { get => telefono; }
        public string Datosreferenciadireccion { get => datosreferenciadireccion; }
    }

