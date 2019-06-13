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
                case "F":
                    while (true) {
                        compile(fileInput());   
                    }
            }
        }

        private void compile(LinkedList<Line> code) {
            Parser parser = new Parser(code);
            int pError = parser.parse();
            Console.WriteLine("Syntax analysis complete.");
            Console.WriteLine();
            if (pError == 0) {
                Contextualiser analyser = new Contextualiser(parser.getRoot());
                analyser.analyse();
                Console.WriteLine("Contextual analysis complete.");
            }
            Console.WriteLine("================================================");
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
    }
}