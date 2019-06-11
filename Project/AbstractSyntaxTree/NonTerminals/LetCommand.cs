using System.Collections;
using System.Collections.Generic;
using Project.AbstractSyntaxTree.NonTerminals;
using Project.NonTerminals;

namespace Project.NonTerminals
{
    public class LetCommand : Command {
        private LinkedList<DeclarationCommand> sequentialDeclaration;
        private LinkedList<Command> sequentialCommand;

        public LetCommand() {
            sequentialDeclaration = new LinkedList<DeclarationCommand>();
            sequentialCommand = new LinkedList<Command>();
        }

        public void addDeclaration(DeclarationCommand declarationCommand) {
            sequentialDeclaration.AddLast(declarationCommand);
        }

        public void addCommand(Command command) {
            sequentialCommand.AddLast(command);
        }

        public LinkedList<DeclarationCommand> SequentialDeclaration => sequentialDeclaration;

        public LinkedList<Command> SequentialCommand => sequentialCommand;
    }
}