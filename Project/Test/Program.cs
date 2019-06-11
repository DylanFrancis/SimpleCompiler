using System;
using System.Collections.Generic;
using System.IO;

namespace Project {
    internal class Program {
        public static void Main(string[] args) {
            Program _program = new Program();
            _program.start(args[0]);
//            Console.WriteLine("Textfile(T) or console(C)?");
//            LinkedList<string> lines;
//            var op = Console.ReadLine();
//            if (op == "T") lines = textfileInput();
//            else lines = consoleInput();


//            Parser P = new Parser("a - ( b * c )");
//            Console.ReadLine();
        }

        private void start(string input) {
            switch (input) {
                case "F": compile(fileInput());
                    break;
                case "C": compile(consoleInput());
                    break;
            }
        }

        private void compile(LinkedList<string> code) {
            Parser p = new Parser(code);
            Console.ReadLine();
        }

        private LinkedList<string> fileInput() {
            LinkedList<string> code = new LinkedList<string>();
            var lines = File.ReadLines("code.txt");
            foreach (string line in lines) {
                code.AddLast(line);
            }
            return code;
        }

        private LinkedList<string> consoleInput() {
            var input = "";
            var code = new LinkedList<string>();
            while (true) {
                input = Console.ReadLine();
                if (input == "-1") return code;
                code.AddLast(input);
            }
        }
    }
}