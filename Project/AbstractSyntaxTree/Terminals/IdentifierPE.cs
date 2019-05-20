namespace Project {
    public class IdentifierPE : PrimaryExpression {
        private Terminal T;

        public IdentifierPE(Terminal T) {
            this.T = T;
        }
    }
}