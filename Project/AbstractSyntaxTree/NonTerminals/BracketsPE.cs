namespace Project {
    public class BracketsPE : PrimaryExpression {
        private Expression E;

        public BracketsPE(Expression E) {
            this.E = E;
        }
    }
}