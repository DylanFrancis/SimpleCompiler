namespace Project.AbstractSyntaxTree.NonTerminals {
    public class DeclarationCommand : Command {
        private Identifier identifier;
        private string type;


        public DeclarationCommand(int line, Identifier identifier) : base(line) {
            this.identifier = identifier;
        }

        public string Type {
            get => type;
            set => type = value;
        }

        public string getName() {
            return identifier.Name;
        }
    }
}