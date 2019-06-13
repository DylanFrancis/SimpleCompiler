using Project.NonTerminals;

namespace Project.NonTerminals
{
    public class AssignCommand : Command {
        private IdentifierPE _identifier;
        private PrimaryExpression _assignTo;
        private Expression _expression;


        public AssignCommand(int line, IdentifierPE identifier) : base(line) {
            _identifier = identifier;
        }

        public IdentifierPE Identifier => _identifier;

        public PrimaryExpression AssignTo {
            get => _assignTo;
            set => _assignTo = value;
        }

        public Expression Expression {
            get => _expression;
            set => _expression = value;
        }
    }
}