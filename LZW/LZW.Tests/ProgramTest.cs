using LZW;

namespace LZW.Tests;

[TestFixture]
[TestOf(typeof(Program))]
public class ProgramTest
{

    [TestCase("../../../Text/Война.txt")]
    public void EncodeFileRuText(string path)
    {
        var input = File.ReadAllBytes(path);
        Program.Encode(path);
        Program.Decode(path + ".zipped");

        var output = File.ReadAllBytes(path);

        Assert.That(output.SequenceEqual(input), Is.True);
    }
    
    [TestCase("../../../Text/War and Peace")]
    public void EncodeFileEnText(string path)
    {
        var input = File.ReadAllBytes(path);
        Program.Encode(path);
        Program.Decode(path + ".zipped");

        var output = File.ReadAllBytes(path);

        Assert.That(output.SequenceEqual(input), Is.True);
    }
    
    [TestCase("../../../Text/Rickrolling.gif")]
    public void EncodeFileGif(string path)
    {
        var input = File.ReadAllBytes(path);
        Program.Encode(path);
        Program.Decode(path + ".zipped");

        var output = File.ReadAllBytes(path);

        Assert.That(output.SequenceEqual(input), Is.True);
    }
}