using System;
using System.Runtime.InteropServices;
using System.Text;
using MyConsoleLibrary;

class Program
{
    static void Main(){
        ConsoleWriter.MyWriteLine("Writing to stdout");
        ConsoleWriter.MyWriteLineError("Writing to stderr");
        ConsoleWriter.MyWrite("No newline");
        ConsoleWriter.MyWriteLine("But this is a newline");
        Console.WriteLine("This is the standard console output");
        Console.Error.WriteLine("This is the standard console error");
    }
}
