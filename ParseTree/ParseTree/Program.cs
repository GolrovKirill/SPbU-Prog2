// See https://aka.ms/new-console-template for more information
using ParseTree;

Console.WriteLine("Input path: ");
var path = Console.ReadLine();
var expression = CorrectionString.Correction(CorrectionString.ReadPath(path));

var tree = new ParseTree.ParseTree();
tree.CreatTree(expression);

Console.WriteLine(tree.ResultExpression());