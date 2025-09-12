using System.Data;
using System.Text.Json;

public interface IAccesoADatos
{
    Cadeteria CargarCadeteria(string ArchivoCadeteria);
    List<Cadete> CargarCadetes(string ArchivoCadete);
}

public class AccesoADatosCSV : IAccesoADatos
{
    public Cadeteria CargarCadeteria(string ArchivoCadeteria)
    {
        if (File.Exists(ArchivoCadeteria))
        {
            string nombre = string.Empty;
            long telefono = 0;

            var lineas = File.ReadAllLines(ArchivoCadeteria);

            foreach (var linea in lineas)
            {
                int indiceComa = linea.IndexOf(';');
                if (indiceComa > 0)
                {
                    string propiedad = linea.Substring(0, indiceComa).Trim().ToLower();
                    string valor = linea.Substring(indiceComa + 1).Trim();

                    switch (propiedad)
                    {
                        case "nombre":
                            nombre = valor;
                            break;

                        case "telefono":
                            if (long.TryParse(valor, out long numero))
                            {
                                telefono = numero;
                            }
                            else
                            {
                                Console.WriteLine("Error: Debes ingresar un número entero válido.");
                            }
                            break;

                    }
                }
            }

            Cadeteria cadeteria = new Cadeteria(nombre, telefono);
            return cadeteria;
        }
        else
        {
            Console.WriteLine("El Archivo No Existe");
            return null;
        }
    }

    public List<Cadete> CargarCadetes(string ArchivoCadete)
    {
        var cadetes = new List<Cadete>();
        if (File.Exists(ArchivoCadete))
        {
            var lineasCadetes = File.ReadAllLines(ArchivoCadete);

            for (int i = 1; i < lineasCadetes.Length; i++)
            {
                var linea = lineasCadetes[i];
                var partes = linea.Split(';');
                if (partes.Length == 4)
                {
                    if (int.TryParse(partes[0], out int id) &&
                        long.TryParse(partes[3], out long telefonoCadete))
                    {
                        string nombreCadete = partes[1];
                        string direccion = partes[2];

                        cadetes.Add( new Cadete (id, nombreCadete, direccion, telefonoCadete));
                    }
                    else
                    {
                        Console.WriteLine($"Error al convertir datos en la línea {i + 1}");
                    }
                }
            }
        }
        return cadetes;
    }
}

public class AccesoADatosJSON : IAccesoADatos
{
    public Cadeteria CargarCadeteria(string ArchivoCadeteria)
    {
        if (File.Exists(ArchivoCadeteria))
        {
            var json = File.ReadAllText(ArchivoCadeteria);
            var datos = JsonSerializer.Deserialize < CadeteriaData > (json);
            Cadeteria cadeteriia = new Cadeteria(datos.Nombre, datos.Telefono);
            return cadeteriia;
        }
        else
        {
            Console.WriteLine("El Archivo no existe");
            return null;
        }

    }

    public List<Cadete> CargarCadetes(string ArchivoCadete)
    {
        if (File.Exists(ArchivoCadete))
        {
            var json = File.ReadAllText(ArchivoCadete);
            var datos = JsonSerializer.Deserialize<List<CadeteData>>(json);
            var cadetes = new List<Cadete>();

            if (datos != null)
            {
                foreach (var item in datos)
                {
                    cadetes.Add(new Cadete(item.Id, item.Nombre, item.Direccion, item.Telefono));
                }
            }
            return cadetes;
        }
        else
        {
            Console.WriteLine("Archivo no encontrado");
            return new List<Cadete>();
        }
    }

    private class CadeteriaData
    {
        public string Nombre { get; set; }
        public long Telefono { get; set; }
    }

    private class CadeteData
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
    }
}

