namespace Project {
    public class AST {
        private int line = -1;

        public AST(int line) {
            this.line = line;
        }

        public int Line {
            get => line;
            set => line = value;
        }
    }
    public class PrimaryExpression : AST {
        public PrimaryExpression(int line) : base(line) {
        }
    }
    public class Command : AST{
        public Command(int line) : base(line) {
        }
    }
}