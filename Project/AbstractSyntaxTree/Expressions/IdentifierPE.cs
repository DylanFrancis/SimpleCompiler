namespace Project {
    public class IdentifierPE : PrimaryExpression
    {
        Terminal T;

        public IdentifierPE(Terminal T)
        {
            this.T = T;
        }
    }
}