namespace Routers;

public static class Depth
{
    /// <summary>
    /// Checks whether all vertices are accessible using a depth-first search..
    /// </summary>
    /// <param name="graph">Used graph.</param>
    /// <returns>True if all vertices are reachable, else false.</returns>
    public static bool CheckReachable(Graph graph)
    {
        var visited = new HashSet<Vertex>();
        var stack = new Stack<Vertex>();

        var vertex = graph.Vertices.ElementAt(0);

        stack.Push(vertex);
        while (stack.Count > 0)
        {
            var currentVertex = stack.Pop();
            visited.Add(currentVertex);

            foreach (var edge in graph.Edges.Where(edge => edge.FirstVertex == currentVertex && !visited.Contains(edge.SecondVertex)))
            {
                stack.Push(edge.SecondVertex);
            }
        }

        return visited.Count == graph.Vertices.Count;
    }
}