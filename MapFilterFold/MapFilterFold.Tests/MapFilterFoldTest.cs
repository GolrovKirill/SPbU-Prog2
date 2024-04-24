namespace MapFilterFold.Tests;

[TestFixture]
[TestOf(typeof(MapFilterFold))]
public class MapFilterFoldTest
{

    [Test]
    public void TestMapWithNumber()
    {
        double AddOne(double value) => value + 1;
        List<double> list = [0, 1, 2, 3, 4];
        
        List<double> resList = [1, 2, 3, 4, 5];
        Assert.That(MapFilterFold.Map(list, AddOne), Is.EqualTo(resList));
    }
    
    [Test]
    public void TestMapWithString()
    {
        string AddOne(string value) => value + "1";
        List<string> list = ["00", "11", "22", "33", "44"];
        
        List<string> resList = ["001", "111", "221", "331", "441"];
        Assert.That(MapFilterFold.Map(list, AddOne), Is.EqualTo(resList));
    }
    
    [Test]
    public void TestFilterWithNumber()
    {
        bool Crat(decimal value) => (value % 2 == 0);
        List<decimal> list = [0, 1, 2, 3, 4];
        
        List<decimal> resList = [0, 2, 4];
        Assert.That(MapFilterFold.Filter(list, Crat), Is.EqualTo(resList));
    }
    
    [Test]
    public void TestFilterWithChar()
    {
        bool Crat(char value) => value is 'a' or 'b' or 'c';
        List<char> list = ['a', 'v', 'g', 'c', 'b'];
        
        List<char> resList = ['a', 'c', 'b'];
        Assert.That(MapFilterFold.Filter(list, Crat), Is.EqualTo(resList));
    }
    
    [Test]
    public void TestFoldWithString()
    {
        string Func(string value, string current) => current + value.ToUpper();
        List<string> list = ["a", "c", "b"];
        
        const string res = "ACB";
        Assert.That(MapFilterFold.Fold(list,  "", Func), Is.EqualTo(res));
    }
    
    [Test]
    public void TestFoldWithNumber()
    {
        decimal Func(decimal value, decimal current) => current * value;
        List<decimal> list = [7, 7, 7];
        
        const decimal res = 343;
        Assert.That(MapFilterFold.Fold(list,  1, Func), Is.EqualTo(res));
    }
}