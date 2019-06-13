using System;
using System.Collections.Generic;
using System.IO;

namespace Project {
    internal class Program {
        public static void Main(string[] args) {
            Program _program = new Program();
            _program.start(args[0]);
        }

        private void start(string input) {
            switch (input) {
                case "F": compile(fileInput());
                    break;
//                case "C": compile(consoleInput());
//                    break;
            }
        }

        private void compile(LinkedList<Line> code) {
            Parser parser = new Parser(code);
            Contextualiser analyser = new Contextualiser(parser.getRoot());
            analyser.analyse();
            Console.WriteLine("Complete.");
            Console.ReadLine();
        }

        private LinkedList<Line> fileInput() {
            LinkedList<Line> code = new LinkedList<Line>();
            var lines = File.ReadLines("code.txt");
            int count = 0;
            foreach (string line in lines) {
                count++;
                code.AddLast(new Line(line, count));
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