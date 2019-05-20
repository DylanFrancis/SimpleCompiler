using Project.NonTerminals;

namespace Project.NonTerminals
{
    public class IfCommand : Command
    {
        private Expression expression;
        private Command thenCommand;
        private Command elseCommand;
    }
}