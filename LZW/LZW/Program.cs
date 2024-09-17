using System.Text;

namespace LZW;

public class Program
{
    public static double Encode(string path)
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException("The file was not opened");
        }

        var file = File.ReadAllBytes(path);
        ArgumentException.ThrowIfNullOrEmpty(nameof(file));

        var newFile = new byte[file.Length];

        file.CopyTo(newFile, 0);

        var newPath = path + ".zipped";
        File.WriteAllBytes(newPath, EncoderLZW.Encoder(newFile));

        var initialFile = new FileInfo(path).Length;
        var outputFile = new FileInfo(newPath).Length;

        return (double)initialFile / (double)outputFile;
    }

    public static void Decode(string path)
    {
        if (path == string.Empty)
        {
            throw new ArgumentException("File path not entered");
        }

        if (!File.Exists(path))
        {
            throw new ArgumentException("The file was not opened");
        }

        ArgumentException.ThrowIfNullOrEmpty(nameof(path));

        var decode = DecoderLZW.Decoder(File.ReadAllBytes(path));

        var newFilePath = path[..path.LastIndexOf('.')];
        File.WriteAllBytes(newFilePath, decode);
    }

    public static void Main()
    {
        Console.Write("Enter the path to the file: ");
        var path = Console.ReadLine();

        Console.Write("Enter the -c option to compress or -u to decompress the file: -");
        var key = Console.ReadLine();

        switch (key)
        {
            case "c":
                var size = Encode(path);
                Console.WriteLine($"Size file {size}");
                break;
            case "u":
                Decode(path);
                Console.WriteLine("Successfully decoded");
                break;
            default:
                Console.WriteLine("Error input key");
                break;
        }
    }
}