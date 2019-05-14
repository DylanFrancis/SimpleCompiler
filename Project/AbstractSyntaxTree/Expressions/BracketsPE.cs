namespace Project {
    public class BracketsPE : PrimaryExpression
    {
        Expression E;

        public BracketsPE(Expression E)
        {
            this.E = E;
        }
    }
}