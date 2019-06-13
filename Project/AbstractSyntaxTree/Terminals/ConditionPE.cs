namespace Project {
    public class ConditionPE : PrimaryExpression{
        private Terminal T;


        public ConditionPE(int line, Terminal t) : base(line) {
            T = t;
        }

        public string getName() {
            return T.Name;
        }
    }
}