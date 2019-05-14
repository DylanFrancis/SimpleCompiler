using System;

namespace Project {
    public class Terminal : AST
    {
        String Spelling;

        public Terminal(String Spelling)
        {
            this.Spelling = Spelling;
        }
    }
}