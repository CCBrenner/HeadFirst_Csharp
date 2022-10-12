using System.IO;

static void Main(string[] args)
{
    var firstline = "No first line was read.";
    try
    {
        var lines = File.ReadAllLines(args[0]);
        firstline = (lines.Length > 0) ? lines[0] : "The file was empty";
    }
    catch (IndexOutOfRangeException)
    {
        Console.Error.WriteLine("Please specify a filename.");
    }
    catch (FileNotFoundException)
    {
        Console.Error.WriteLine("Unable to find file: {0}", args[0]);  // Could be prevented by using File.Exists() instead
    }
    catch (UnauthorizedAccessException ex)
    {
        Console.Error.WriteLine("File {0} could not be accessed: {1}", args[0], ex.Message);
    }
    finally
    {
        Console.WriteLine(firstline);
    }
}