using Project.NonTerminals;

namespace Project.NonTerminals
{
    public class IfCommand : Command{
        private Expression expression;
        private Command thenCommand;
        private Command elseCommand;

        public IfCommand(Expression expression) {
            this.expression = expression;
        }

        public IfCommand(Expression expression, Command thenCommand, Command elseCommand) {
            this.expression = expression;
            this.thenCommand = thenCommand;
            this.elseCommand = elseCommand;
        }

        public Expression Expression {
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