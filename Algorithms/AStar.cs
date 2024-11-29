using SnowRoutesApp.Models;

public static class AStar
{
    public static List<GraphNode> FindPath(GraphNode start, GraphNode end, Func<GraphNode, double> heuristic)
    {
        var openSet = new HashSet<GraphNode> { start };
        var cameFrom = new Dictionary<GraphNode, GraphNode>();

        var gScore = new Dictionary<GraphNode, double> { [start] = 0 };
        var fScore = new Dictionary<GraphNode, double> { [start] = heuristic(start) };

        while (openSet.Count > 0)
        {
            var current = openSet.OrderBy(node => fScore.GetValueOrDefault(node, double.PositiveInfinity)).First();

            if (current == end)
            {
                var path = new List<GraphNode>();
                while (cameFrom.ContainsKey(current))
                {
                    path.Insert(0, current);
                    current = cameFrom[current];
                }
                path.Insert(0, start);
                return path;
            }

            openSet.Remove(current);

            foreach (var edge in current.Neighbors)
            {
                var neighbor = edge.To;
                var tentativeGScore = gScore.GetValueOrDefault(current, double.PositiveInfinity) + edge.Weight;

                if (tentativeGScore < gScore.GetValueOrDefault(neighbor, double.PositiveInfinity))
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + heuristic(neighbor);
                    openSet.Add(neighbor);
                }
            }
        }

        return new List<GraphNode>(); // Si no hay ruta, retornamos una lista vacía
    }
}
