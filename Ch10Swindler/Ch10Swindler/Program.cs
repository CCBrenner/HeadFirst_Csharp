using System.IO;

class Program
{
    public static void Main(string[] args)
    {
        // This code writes a new file in the project.
        // It's found in bin/Debug/net6.0
        // Delete the file and see it get created again with the content that this code generates.
         
        StreamWriter sw = new StreamWriter("secret_plan.txt");

        sw.WriteLine("How I'll defeat Captain Amazing");
        sw.WriteLine("Another genius secret plan by the Swindler");
        sw.WriteLine("I'll unleash my army of clones on the citizens of Objectville");

        string location = "the mall";
        for (int number = 0; number <= 5; number++)
        {
            sw.WriteLine("Clone #{0} attacks {1}", number, location);
            location = (location == "the mall") ? "downtown" : "the mall";
        }
        sw.Close();
    }
}