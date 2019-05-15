using System;
using System.Collections.Generic;

namespace Project {
    internal class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Textfile(T) or console(C)?");
            LinkedList<string> lines;
            var op = Console.ReadLine();
            if (op == "T") lines = textfileInput();
            else lines = consoleInput();


//            Parser P = new Parser("a - ( b * c )");
//            Console.ReadLine();
        }

        private static LinkedList<string> textfileInput() {
            return null;
        }

        private static string[] consoleOneLine() {
            var lines = Console.ReadLine().Split(';');
            return lines;
        }

        private static LinkedList<string> consoleInput() {
            var input = "";
            var lines = new LinkedList<string>();
            while (true) {
                input = Console.ReadLine();
                if (input == "-1") return lines;
                lines.AddLast(input);
            }
        }
    }
}