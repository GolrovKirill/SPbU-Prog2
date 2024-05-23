namespace Routers;

using System.Text;

/// <summary>
/// Vertex in the graph.
/// </summary>
public record Vertex(int Router)
{
    /// <summary>
    /// Gets the number of the router.
    /// </summary>
    public int Router { get; set; } = Router;
}

/// <summary>
/// Edge in the graph.
/// </summary>
public record Edge(Vertex firstVertex, Vertex secondVertex, int bandwidth)
{
    /// <summary>
    /// Gets the first vertex.
    /// </summary>
    public Vertex FirstVertex { get; set; } = firstVertex;

    /// <summary>
    /// Gets the second vertex.
    /// </summary>
    public Vertex SecondVertex { get; set; } = secondVertex;

    /// <summary>
    /// Gets the bandwidth.
    /// </summary>
    public int Bandwidth { get; set; } = bandwidth;
}

public class Graph
{
    /// <summary>
    /// Gets the set of vertices in the graph.
    /// </summary>
    public HashSet<Vertex> Vertices { get; set; } = [];

    /// <summary>
    /// Gets the list of edges in the graph.
    /// </summary>
    public List<Edge> Edges { get; set; } = [];

    /// <summary>
    /// Builds the graph from the topology.
    /// </summary>
    /// <param name="path">The path to the desired file.</param>
    public void BuildGraph(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        var topology = File.ReadAllText(path).Trim('\n').Split("\n");

        if (topology[0] == string.Empty)
        {
            throw new ExceptionReadFile();
        }

        foreach (var symbol in topology)
        {
            this.ParseInputString(symbol);
        }
    }

    /// <summary>
    /// Adds an edge.
    /// </summary>
    /// <param name="edge">The edge to add.</param>
    public void AddEdge(Edge edge)
    {
        this.Edges.Add(edge);
        this.Vertices.Add(edge.FirstVertex);
        this.Vertices.Add(edge.SecondVertex);
    }

    /// <summary>
    /// Writes to a file.
    /// </summary>
    /// <param name="path">The path to the desired file.</param>
    public void Write(string path)
    {
        File.WriteAllText(path, this.ToString());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        var sortedEdges = Edges.OrderBy(e => e.FirstVertex.Router).ToList();

        var i = 0;
        while (i < sortedEdges.Count)
        {
            stringBuilder.Append($"{sortedEdges[i].FirstVertex.Router}: ");

            while (i + 1 < sortedEdges.Count)
            {
                if (sortedEdges[i].FirstVertex != sortedEdges[i + 1].FirstVertex)
                {
                    break;
                }

                stringBuilder.Append($"{sortedEdges[i].SecondVertex.Router} ({sortedEdges[i].Bandwidth}), ");
                ++i;
            }

            stringBuilder.Append($"{sortedEdges[i].SecondVertex.Router} ({sortedEdges[i].Bandwidth})\n");
            ++i;
        }

        return stringBuilder.ToString();
    }

    private void ParseInputString(string symbol)
    {
        var input = symbol.Split(':');
        var firstRouter = new Vertex(int.Parse(input[0]));
        var links = input[1].Split(',');
        Vertices.Add(firstRouter);

        foreach (var link in links)
        {
            var band = link.Trim().Split(' ');
            var secondRouter = new Vertex(int.Parse(band[0]));
            
            Vertices.Add(secondRouter);

            var bandWidth = int.Parse(band[1].Trim('(', ')'));

            var edge = new Edge(firstRouter, secondRouter, bandWidth);
            this.Edges.Add(edge);
        }
    }
}