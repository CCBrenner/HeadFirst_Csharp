// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        // OperatorExamples();
        // codeSnippet3();
        // codeSnippet2();
        // TryAnIf();
        // TrySomeLoops();
        TryAnIfElse();
    }

    private static void TryAnIf()
    {
        int someValue = 4;
        string name = "Bobbo Jr.";
        if ((someValue == 3) && (name == "Joe"))
        {
            Console.WriteLine("x is 3 and the name is Joe");
        }
        Console.WriteLine("this line runs no matter what");
    }
    private static void TryAnIfElse()
    {
        int x = 5;
        if (x == 10)
        {
            Console.WriteLine("x must be 10");
        }
        else
        {
            Console.WriteLine("x isn’t 10");
        }
    }

    private static void TrySomeLoops()
    {
        int count = 0;
        while (count < 10)
        {
            count = count + 1;
        }
        for (int i = 0; i < 5; i++)
        {
            count = count - 1;
        }
        Console.WriteLine("The answer is " + count);
    }

    private static void codeSnippet2()
    {
        int j = 2;
        int forCount = 0;
        int whileCount = 0;
        for (int i = 1; i < 100;
             i = i * 2)
        {
            j = j - 1;
            while (j < 25)
            {
                // How many times will
                // the next statement
                // be executed?
                j = j + 5;
                whileCount++;
            }
            forCount++;
        }
        Console.WriteLine("forCount: " + forCount);
        Console.WriteLine("whileCount: " + whileCount);
    }

    private static void codeSnippet3()
    {
        int p = 2;
        int forCount = 0;
        int whileCount = 0;
        for (int q = 2; q < 32;
             q = q * 2)
        {
            while (p < q)
            {
                // How many times will
                // the next statement
                // be executed?
                p = p * 2;
                whileCount++;
            }
            q = p - q;
            forCount++;
        }
        Console.WriteLine("forCount: " + forCount);
        Console.WriteLine("whileCount: " + whileCount);
    }

    private static void OperatorExamples()
    {
        // This is a comment to jump over
        int width = 3;

        width++;

        int height = 2 + 4;
        int area = width * height;
        Console.WriteLine(area);

        // This was template instantiated by typing "while" and pressing Tab twice
        while (area < 50)
        {
            height++;
            area = width * height;
        }

        do
        {
            width--;
            area = width * height;
        } while (height > 20);

        string result = "The area ";
        result = result + "is " + area;
        Console.WriteLine(result);

        bool truthValue = true;
        Console.WriteLine(truthValue);
    }
}

// Console.WriteLine("Hello, World!");
