namespace Router.Tests;
using Routers;

public class Tests
{
    [Test]
    public void BuildGraphFromFileIsEmpty()
    {
        var graph = new Graph();
        const string path = "../../../../TestSet/Empty.txt";

        Assert.Throws<ExceptionReadFile>(() => graph.BuildGraph(path));
    }

    [Test]
    public void BuildGraphTopology1()
    {
        var graph = new Graph();
        graph.BuildGraph("../../../../TestSet/test1.txt");
        
        Assert.Multiple(() =>
        {
            Assert.That(graph.Vertices, Has.Count.EqualTo(5));
            Assert.That(graph.Edges, Has.Count.EqualTo(7));
        });
    }

    [Test]
    public void KruskalForTest1()
    {
        var graph = new Graph();
        graph.BuildGraph("../../../../TestSet/test1.txt");

        var kruskalResult = Kruskal.StartKruskal(graph);

        int[] expectedVertices = [1, 2, 3, 4, 5];
        int[] expectedEdgesBandwidths = [3, 5, 6, 7];

        foreach (var vertex in kruskalResult.Vertices)
        {
            Assert.That(expectedVertices, Does.Contain(vertex.Router));
        }

        foreach (var edge in kruskalResult.Edges)
        {
            Assert.That(expectedEdgesBandwidths, Does.Contain(edge.Bandwidth));
        }
    }

    [Test]
    public void BuildGraphTopology2()
    {
        var graph = new Graph();
        graph.BuildGraph("../../../../TestSet/test2.txt");

        Assert.That(Depth.CheckReachable(graph), Is.False);
    }
}