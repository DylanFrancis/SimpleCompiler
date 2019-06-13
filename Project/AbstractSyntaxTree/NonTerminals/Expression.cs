namespace Project {
    public class Expression : Command {
        protected Operator O;
        protected PrimaryExpression P1;
        protected PrimaryExpression P2;


        public Expression(int line, PrimaryExpression p1, Operator o, PrimaryExpression p2) : base(line) {
            O = o;
            P1 = p1;
            P2 = p2;
        }

        public Operator GetOperator() {
            return O;
        }

        public void setOperator(Operator value) {
            O = value;
        }

        public PrimaryExpression getP1 {
            get => P1;
            set => P1 = value;
        }

        public PrimaryExpression getP2 {
            get => P2;
            set => P2 = value;
        }
    }
}