using System;
using Spectre.Console;

namespace ie_Setup_By_UniquezHD
{
    class Misc
    {
        public static void Credits()
        {
            Console.ForegroundColor = Color.Cyan1;
            Console.WriteLine("");
            Console.WriteLine("                           ██╗   ██╗███╗   ██╗██╗ ██████╗ ██╗   ██╗███████╗███████╗██╗  ██╗██████╗ ");
            Console.WriteLine("                           ██║   ██║████╗  ██║██║██╔═══██╗██║   ██║██╔════╝╚══███╔╝██║  ██║██╔══██╗");
            Console.WriteLine("                           ██║   ██║██╔██╗ ██║██║██║   ██║██║   ██║█████╗    ███╔╝ ███████║██║  ██║");
            Console.WriteLine("                           ██║   ██║██║╚██╗██║██║██║▄▄ ██║██║   ██║██╔══╝   ███╔╝  ██╔══██║██║  ██║");
            Console.WriteLine("                           ╚██████╔╝██║ ╚████║██║╚██████╔╝╚██████╔╝███████╗███████╗██║  ██║██████╔╝");
            Console.WriteLine("                            ╚═════╝ ╚═╝  ╚═══╝╚═╝ ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝╚═╝  ╚═╝╚═════╝");
        }

        public static void Hash(String text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n[");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("#");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("] ");
            Console.Write(text);
        }
    }
}
