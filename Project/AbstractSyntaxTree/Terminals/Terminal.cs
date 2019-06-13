namespace Project {
    public class Terminal : AST {
        private string Spelling;


        public Terminal(int line, string spelling) : base(line) {
            Spelling = spelling;
        }

        public string Name => Spelling;
    }
}