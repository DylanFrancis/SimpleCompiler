using System.Collections.Generic;

namespace Project {
    public class ConditionExpression : Command {
        protected Condition C;
        protected AST P1;
        protected AST P2;


        public ConditionExpression(int line, AST p1, Condition c, AST p2) : base(line) {
            C = c;
            P1 = p1;
            P2 = p2;
        }

        public Condition C1 {
            get => C;
            set => C = value;
        }

        public AST AST1 {
            get => P1;
            set => P1 = value;
        }

        public AST AST2 {
            get => P2;
            set => P2 = value;
        }
    }
}