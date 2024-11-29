namespace SnowRoutesApp.Models
{
    public class Edge
    {
        public GraphNode From { get; set; } = null!;
        public GraphNode To { get; set; } = null!;
        public double Weight { get; set; }

        public Edge(GraphNode from, GraphNode to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}
