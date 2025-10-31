using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyConsoleLibrary{
    public static class ConsoleWriter{
        [DllImport("libc", SetLastError = true)]
        private static extern int write(int fd, byte[] buf, int count);

        private const int STDOUT_FILENO = 1;
        private const int STDERR_FILENO = 2;

        public static void MyWriteLine(string text){
            MyWrite("\n" + text + "\n");
        }

        public static void MyWrite(string text){
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            int result = write(STDOUT_FILENO, bytes, bytes.Length);
            if (result == -1){
                throw new InvalidOperationException("Failed to write to stdout");
            }
        }

        public static void MyWriteLineError(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text + "\n");
            write(STDERR_FILENO, bytes, bytes.Length);
        }
    }
}