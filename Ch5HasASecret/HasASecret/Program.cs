using System.Reflection;

// This program demonstrates how private fields can still be accessed and therefore
// are not security but rather are refactoring improvements (makes things simpler,
// easier to to understand, easier to use, prevent bugs).

class HasASecret
{
    // This class has a private field. Does the private keyword make it secure?
    private string secret = "xyzzy";
}
class MainClass
{
    public static void Main(string[] args)
    {
        HasASecret keeper = new HasASecret();
        // Uncommenting this Console.WritelIne statement causes a compiler error:
        // 'HasASecret.secret' is inaccessible due to its protection level
        // Console.WriteLine(keeper.secret);
        // But we can still use reflection to get the value of the secret field
        FieldInfo[] fields = keeper.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        // This foreach loop will cause "xyzzy" to be printed to the console
        foreach (FieldInfo field in fields)
        {
            Console.WriteLine(field.GetValue(keeper));
        }
    }
}
