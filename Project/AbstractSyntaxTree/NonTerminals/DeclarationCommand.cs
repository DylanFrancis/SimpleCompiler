namespace Project.AbstractSyntaxTree.NonTerminals {
    public class DeclarationCommand : Command {
        private Identifier identifier;
        private string type;

        public DeclarationCommand(Identifier identifier, string type) {
            this.identifier = identifier;
            this.type = type;
        }

        public DeclarationCommand(Identifier identifier) {
            this.identifier = identifier;
        }
        
        public string Type {
            get => type;
            set => type = value;
        }
    }
}