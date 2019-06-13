using Project.NonTerminals;

namespace Project.NonTerminals
{
    public class IfCommand : Command{
        private ConditionExpression expression;
        private Command thenCommand;
        private Command elseCommand;


        public IfCommand(int line, ConditionExpression expression) : base(line) {
            this.expression = expression;
        }

        public ConditionExpression Expression {
            get => expression;
            set => expression = value;
        }

        public Command ThenCommand {
            get => thenCommand;
            set => thenCommand = value;
        }

        public Command ElseCommand {
            get => elseCommand;
            set => elseCommand = value;
        }
    }
}