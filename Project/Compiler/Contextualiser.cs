using System;
using System.Collections.Generic;
using Project.AbstractSyntaxTree.NonTerminals;
using Project.NonTerminals;

namespace Project {
    public class Contextualiser : Compiler {
        private LetCommand root;
        private Stack<LinkedList<DeclarationCommand>> declaStack;
        private HashSet<string> variables;
        
        public Contextualiser(LetCommand root) {
            this.root = root;
            declaStack = new Stack<LinkedList<DeclarationCommand>>();
            variables = new HashSet<string>();
        }

        public void analyse() {
            DoAnalyse(root);
        }

        private void DoAnalyse(Command node) {
            if (node is LetCommand) {
                DeclarationCheck(((LetCommand)node).SequentialDeclaration);
                AnalyzeCommands(((LetCommand)node).SequentialCommand);
            }

            if (node is AssignCommand) {
                
            }

            if (node is IfCommand) {
                
            }
        }

        private void AnalyzeCommands(LinkedList<Command> commands) {
            foreach (Command command in commands) {
                DoAnalyse(command);
            }
        }

        /**
         * Checks if new variables have already been declared in current scope
         */
        private void DeclarationCheck(LinkedList<DeclarationCommand> declarationCommands) {
            foreach (DeclarationCommand declaration in declarationCommands) {
                string id = declaration.getName();
                if (variables.Contains(id)) {
                    Console.WriteLine("Contextual Error: variable \"" + id + "\" is already declared in scope.");
                }
                else {
                    variables.Add(id);
                }
            }
            declaStack.Push(declarationCommands);
        }
        
        /**
         * Rebuilds variable set when a Let is removed
         */
        private void rebuildVariables() {
            variables.Clear();
            foreach (LinkedList<DeclarationCommand> declarationCommands in declaStack) {
                foreach (DeclarationCommand declaration in declarationCommands) {
                    variables.Add(declaration.getName());
                }
            }   
        }

        private void checkVariableType() {
            
        }
        
        private void StackIterator() {
            foreach (LinkedList<DeclarationCommand> declarationCommands in declaStack) {
                foreach (DeclarationCommand declaration in declarationCommands) {
                    
                }
            }
        }
    }
}