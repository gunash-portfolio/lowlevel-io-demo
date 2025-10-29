using System;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    [DllImport("libc", SetLastError = true)]
    static extern int write(int fd, byte[] buf, int count);

    const int STDOUT_FILENO = 1;
    const int STDERR_FILENO = 2;

    static void Main()
    {
        MyWriteLine("Hello Wolrd");
        MyWriteLine("This is written from scratch");
        MyWriteLine("No Console.WriteLine used here!");


        MyWrite("This doesn't add a newline:");
        MyWriteLine("But this does!");


        MyWriteLineError("This goes to error output!");
    }
    static void MyWriteLine(string text)
    {
        MyWrite(text + "\n");
    }
    static void MyWrite(string text)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        int result = write(STDOUT_FILENO, bytes, bytes.Length);
        if (result == -1)
        {
            throw new InvalidOperationException("Failed to write to stdout");
        }


    }
    static void MyWriteLineError(string text)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text + "\n");
        write(STDERR_FILENO, bytes, bytes.Length);
    }
}
