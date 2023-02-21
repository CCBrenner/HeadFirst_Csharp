// See https://aka.ms/new-console-template for more information

/*
 *
float myFloatValue = 10;
int myIntvalue = (int) myFloatValue;
Console.WriteLine("myIntValue is " + myIntvalue);
*/

/*
int myInt = 10;

byte myByte = (byte)myInt;

double myDouble = (double)myByte;

// bool myBool = (bool)myDouble;

string myString = "false";

// myBool = (bool)myString;

// myString = (string)myInt;

myString = myInt.ToString();

// myBool = (bool)myByte;

// myByte = (byte)myBool;

short myShort = (short)myInt;

char myChar = 'x';

// myString = (string)myChar;

long myLong = (long)myInt;

decimal myDecimal = (decimal)myLong;
myString = myString + myInt + myByte + myDouble + myChar;
*/

/*
float myFloat = 7.0F;
string chaos = "Chaos!";
chaos = "Chaos " + myFloat;
chaos = myFloat.ToString();
Console.WriteLine(chaos);
int newFloat = (int)myFloat;
Console.WriteLine(Convert.ToString(newFloat, 2));
*/

/*
using System;
class Program
{
    public static void Main()
    {
        int done = MyMethod(false);
        Console.WriteLine(done);
        Console.ReadLine();
    }
    public static int MyMethod(bool add3)
    {
        int value = 12;

        if (add3)
            value += 3;
        else
            value -= 2;

        return value;

    }
}
*/

static int MyMethod(bool add3)
{
    int value = 12;

    if (add3)
        value += 3;
    else
        value -= 2;

    return value;

}
int done = MyMethod(false);
Console.WriteLine(done);
Console.ReadLine();