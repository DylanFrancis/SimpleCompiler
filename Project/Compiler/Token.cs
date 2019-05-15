using System;

namespace Project {
    public class Token {
        private readonly string Spelling;
        private readonly int Type; // 1 - Identifier, 2 Operator, 3 LPar, 4 RPar

        public Token(string S, int T) {
            Spelling = S;
            Type = T;
        }

        public string getSpelling() {
            return Spelling;
        }

        public int getType() {
            return Type;
        }

        public void showSpelling() {
            Console.WriteLine(Spelling);
        }

        public bool matches(string other) {
            return Spelling.Equals(other);
        }

        public bool matchesType(int T) {
            return Type == T;
        }
    }
}