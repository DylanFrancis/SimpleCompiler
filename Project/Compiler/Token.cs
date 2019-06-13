using System;
using System.Collections.Generic;

namespace Project {
    public class Token {
        private readonly string Spelling;
        private readonly int Type; // 1 - Identifier, 2 Operator, 3 LPar, 4 RPar
        private readonly int _line;

        public Token(string S, int T, int L) {
            Spelling = S;
            Type = T;
            _line = L;
        }

        public int Line => _line;

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