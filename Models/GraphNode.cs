namespace SnowRoutesApp.Models
{
    public class GraphNode
    {
        public string? Name { get; set; } // Nombre del nodo
        public double Latitude { get; set; } // Latitud del nodo
        public double Longitude { get; set; } // Longitud del nodo
        public List<Edge> Neighbors { get; set; } = new(); // Lista de aristas (Edge)
    }
}
