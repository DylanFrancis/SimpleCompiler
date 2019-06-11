using Project.NonTerminals;

namespace Project.NonTerminals
{
    public class AssignCommand : Command {
        private Identifier identifier;
        private Terminal assign;
        private Expression expression;

        public AssignCommand(Identifier identifier, Terminal assign) {
            this.identifier = identifier;
            this.assign = assign;
        }

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

        public Terminal Assign {
            get => assign;
            set => assign = value;
        }
    }
}