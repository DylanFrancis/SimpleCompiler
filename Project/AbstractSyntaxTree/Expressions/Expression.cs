namespace Project {
    public class Expression : AST
    {
        public PrimaryExpression P1;
        public Operator O;
        public PrimaryExpression P2;

        public Expression (PrimaryExpression P1, Operator O, PrimaryExpression P2)
        {
            this.P1 = P1; this.O = O; this.P2 = P2;
        }
    }
}