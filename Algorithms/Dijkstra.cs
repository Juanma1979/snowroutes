using SnowRoutesApp.Models;

public static class Dijkstra
{
    public static List<GraphNode> FindShortestPath(GraphNode start, GraphNode end)
    {
        var distances = new Dictionary<GraphNode, double>();
        var previousNodes = new Dictionary<GraphNode, GraphNode>();
        var unvisited = new HashSet<GraphNode>();

        foreach (var node in start.Neighbors.Select(edge => edge.To))
        {
            distances[node] = double.PositiveInfinity;
            unvisited.Add(node);
        }
        distances[start] = 0;

        while (unvisited.Count > 0)
        {
            var current = unvisited.OrderBy(node => distances[node]).FirstOrDefault();
            if (current == null || distances[current] == double.PositiveInfinity)
                break;

            if (current == end)
                break;

            unvisited.Remove(current);

            // Actualizar distancias de los vecinos
            foreach (var edge in current.Neighbors)
            {
                var neighbor = edge.To;
                var alternative = distances[current] + edge.Weight;
                if (alternative < distances[neighbor])
                {
                    distances[neighbor] = alternative;
                    previousNodes[neighbor] = current;
                }
            }
        }

        var path = new List<GraphNode>();
        for (var at = end; at != null; at = previousNodes.GetValueOrDefault(at))
            path.Insert(0, at);

        return path.Any() && path.First() == start ? path : new List<GraphNode>();
    }
}
