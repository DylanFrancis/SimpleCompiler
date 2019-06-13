namespace Project {
    public class IdentifierPE : PrimaryExpression {
        private Terminal T;


        public IdentifierPE(int line, Terminal t) : base(line) {
            T = t;
        }

        public string getName() {
            return T.Name;
        }
    }
}