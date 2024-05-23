namespace Routers;

public static class Kruskal
{
    /// <summary>
    /// Runs the Kruskal's algorithm on the given graph.
    /// </summary>
    /// <param name="graph">Used graph.</param>
    /// <returns>The maximum tree.</returns>
    public static Graph StartKruskal(Graph graph)
    {
        var edges = graph.Edges.OrderByDescending(e => e.Bandwidth).ToList();

        var result = new Graph();

        foreach (var edge in edges.Where(edge => !result.Vertices.Contains(edge.FirstVertex) || !result.Vertices.Contains(edge.SecondVertex)))
        {
            result.AddEdge(edge);
        }

        return result;
    }
}