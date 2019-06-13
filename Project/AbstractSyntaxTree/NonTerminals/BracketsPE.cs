namespace Project {
    public class BracketsPE : PrimaryExpression {
        private Expression E;


        public BracketsPE(int line, Expression e) : base(line) {
            E = e;
        }

        public Expression GetExpression => E;
    }
}