
// Example of how you can use lambdas in place of Func<int,int> 
class Program
{
    public static void Main(string[] args)
    {
        // an array of ints is iterated on to create a new IEnumerable<int>
        // Highlight: array.Select( i => i * 2 )
        int[] array = { 1, 2, 3, 4 };
        IEnumerable<int>? result = array.Select( i => i * 2 );
        foreach(int i in result) Console.WriteLine(i);
    }
}
