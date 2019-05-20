using Project.NonTerminals;

namespace Project.NonTerminals
{
    public class AssignCommand : Command {
        private Identifier identifier;
        private Expression expression;

        public AssignCommand(Identifier identifier, Expression expression) {
            this.identifier = identifier;
            this.expression = expression;
        }

        public AssignCommand(Identifier identifier) {
            this.identifier = identifier;
        }

        public Identifier Identifier => identifier;

        public Expression Expression {
            get => expression;
            set => expression = value;
        }
    }
}