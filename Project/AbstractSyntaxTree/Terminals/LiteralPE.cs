namespace Project {
    public abstract class LiteralPE : PrimaryExpression {
        private Terminal T;

        protected LiteralPE(int line, Terminal t) : base(line) {
            T = t;
        }

        public string getName() {
            return T.Name;
        }
    }
}