using System;
using System.Collections.Generic;
using System.IO;
using Project.AbstractSyntaxTree.NonTerminals;
using Project.NonTerminals;

namespace Project {
    public class Contextualiser : Compiler {
        private LetCommand root;
        private LinkedList<LinkedList<DeclarationCommand>> declaList;
        private Dictionary<string, string> table;
        
        public Contextualiser(LetCommand root) {
            _compilerUtils = CompilerUtils.getInstance();
            this.root = root;
            declaList = new LinkedList<LinkedList<DeclarationCommand>>();
            table = new Dictionary<string, string>();
        }

        public void analyse() {
            DoAnalyse(root);
        }

        private void DoAnalyse(Command node) {
            if (node is LetCommand) {
                LetCommand letCommand = (LetCommand) node;
                DeclarationCheck(letCommand.SequentialDeclaration);
                AnalyseCommands(letCommand.SequentialCommand);
                RemoveScope(letCommand.SequentialDeclaration);
            }

            if (node is AssignCommand) {
                AssignCommand assignCommand = (AssignCommand) node;
                string type = "";
                AnalyseAssignment(assignCommand, type);
            }

            if (node is IfCommand) {
                IfCommand ifCommand = (IfCommand) node;
                AnalyzeCondition(ifCommand.Expression);
                DoAnalyse(ifCommand.ThenCommand);
                DoAnalyse(ifCommand.ElseCommand);
            }
        }

        private void AnalyzeCondition(ConditionExpression conditionExpression) {
            AST ast1 = conditionExpression.AST1;
            AST ast2 = conditionExpression.AST2;
            Condition c = conditionExpression.C1;

            IdentifierPE identifierPe = (IdentifierPE) getFirstPrimaryExpression(ast1);
            if (!variableExists(identifierPe)) {
                return;
            }
            string type = table[identifierPe.getName()];

            if (type == "boolean") {
                switch (c.Name) {
                    case "<":    
                    case ">":    
                    case ">=":    
                    case "<=": 
                        logCError(conditionExpression, "Incorrect condition statement: \"" + c.Name + "\" for: \"" + type + "\"");
                        return; 
                }
            }
            AnalyzeIfAST(ast1, type);
            AnalyzeIfAST(ast2, type);
        }

        private void AnalyzeIfAST(AST ast, string type) {
            if (ast is BracketsPE) {
                Expression expression = ((BracketsPE) ast).GetExpression;
                AnalyzeIfAST(expression.getP1, type);
                AnalyzeIfAST(expression.getP2, type);
                return;
            }
            
            if (ast is Expression) {
                Expression expression = (Expression) ast;
                AnalyzeIfAST(expression.getP1, type);
                AnalyzeIfAST(expression.getP2, type);
            }

            if (ast is ConditionPE || ast is LiteralPE || ast is IdentifierPE) {
                CheckType(type, (PrimaryExpression)ast);
                variableExists((PrimaryExpression)ast); 
            }
        }
        
        private PrimaryExpression getFirstPrimaryExpression(AST ast) {
            if (ast is IdentifierPE || ast is ConditionPE || ast is LiteralPE) {
                return (PrimaryExpression) ast;
            }

            if (ast is BracketsPE) {
                BracketsPE bracketsPe = (BracketsPE) ast;
                return getFirstPrimaryExpression(bracketsPe.GetExpression.getP1);
            }

            if (ast is Expression) {
                return getFirstPrimaryExpression(((Expression) ast).getP1);
            }

            return null;
        }

        private void AnalyseCommands(LinkedList<Command> commands) {
            foreach (Command command in commands) {
                DoAnalyse(command);
            }
        }

        private bool CheckType(string type, PrimaryExpression primaryExpression) {
            if (primaryExpression is IdentifierPE) {
                IdentifierPE identifierPe = (IdentifierPE) primaryExpression;
                if (!table.ContainsKey(identifierPe.getName()))
                    return false;
                if (table[identifierPe.getName()] == type) {
                    return true;
                }

                string[] msg = {"expected: " + type, "given: " + table[identifierPe.getName()]};
                logCError(primaryExpression, msg);   
                return false;
            }
            
            if (primaryExpression is ConditionPE) {
                if (_compilerUtils.Types[type] == _boolean) return true;
                string[] msg = {"expected: " + type, "given: " + _compilerUtils.Syntax[_boolean]};
                logCError(primaryExpression, msg); 
                return false;
            }
            
            if (primaryExpression is LiteralString) {
                if (_compilerUtils.Types[type] == _string) return true;
                string[] msg = {"expected: " + type, "given: " + _compilerUtils.Syntax[_string]};
                logCError(primaryExpression, msg); 
                return false;
            }

            if (primaryExpression is LiteralInt) {
                if (_compilerUtils.Types[type] == _integer) return true;
                string[] msg = {"expected: " + type, "given: " + _compilerUtils.Syntax[_integer]};
                logCError(primaryExpression, msg); 
                return false;
            }
            logCError(primaryExpression, "Unknown error in CheckType");
            return false;
        }

        private void AnalyseAssignment(AssignCommand assignCommand, string type) {
            variableExists(assignCommand.Identifier);
            type = table[assignCommand.Identifier.getName()];
            
            if (assignCommand.Expression != null) {
                Expression expression = assignCommand.Expression;
                AnalysePrimaryExpression(expression.getP1, type);
                AnalysePrimaryExpression(expression.getP2, type);
                AnalyzeOperator(expression.GetOperator(), type);
            }

            if (assignCommand.AssignTo != null) {
                variableExists(assignCommand.AssignTo);
                CheckType(type, assignCommand.AssignTo);
            }
        }
        
        private void AnalysePrimaryExpression(PrimaryExpression expression, string type) {
            if (expression is IdentifierPE) {
                IdentifierPE identifierPe = (IdentifierPE) expression;
                variableExists(identifierPe);
                CheckType(type, identifierPe);
                return;
            }

            if (expression is BracketsPE) {
                BracketsPE bracketsPe = (BracketsPE) expression;
                AnalysePrimaryExpression(bracketsPe.GetExpression.getP1, type);
                AnalysePrimaryExpression(bracketsPe.GetExpression.getP2, type);
                AnalyzeOperator(bracketsPe.GetExpression.GetOperator(), type);
                return;
            }

            if (expression is ConditionPE) {
                ConditionPE conditionPe = (ConditionPE)expression;
                variableExists(conditionPe);   
                if(type != "boolean")
                    logCError(expression, "Boolean illegal in: " + type + "" );
            }
        }

        private void AnalyzeOperator(Operator op, string type) {
            switch (type) {
                case "integer":
                    break;
                case "string":
                    switch (op.Name) {
                        case "*":
                        case "/":
                            string[] msg = {"Illegal operator for: \"" + type + "\"", "given: " + op.Name + "\""};
                            logCError(op, msg);
                            return;
                    }
                    break;
                case "boolean":
                    switch (op.Name) {
                        case "+":
                        case "-":
                        case "*":
                        case "/":
                            string[] msg = {"Illegal operator for: \"" + type + "\"", "given: " + op.Name + "\""};
                            logCError(op, msg);
                            return;
                    }
                    break;
            }
        }

        private bool variableExists(PrimaryExpression primaryExpression) {
            if (primaryExpression is IdentifierPE) {
                if (!table.ContainsKey(((IdentifierPE)primaryExpression).getName())) {
                    logCError(primaryExpression, "variable: \"" + ((IdentifierPE)primaryExpression).getName() + "\" not declared.");
                    return false;
                }
                return true;
            }

            if (primaryExpression is ConditionPE || primaryExpression is LiteralPE) {
                return true;
            }

            return false;
        }
        
        /**
         * Checks if new variables have already been declared in current scope
         */
        private void DeclarationCheck(LinkedList<DeclarationCommand> declarationCommands) {
            foreach (DeclarationCommand declaration in declarationCommands) {
                string id = declaration.getName();
                if (table.ContainsKey(id)) {
                    logCError(declaration, "variable \"" + id + "\" is already declared in scope.");
                }
                else {
                    table.Add(id, declaration.Type);
                }
            }
            declaList.AddLast(declarationCommands);
        }

//        private void AddScope(LinkedList<DeclarationCommand> declarationCommands) {
//            foreach (DeclarationCommand declarationCommand in declarationCommands) {
//                if(!table.ContainsKey(declarationCommand.getName()))
//                    table.Add(declarationCommand.getName(), declarationCommand.Type);
//            }
//        }

        private void RemoveScope(LinkedList<DeclarationCommand> declarationCommands) {
            foreach (DeclarationCommand declarationCommand in declarationCommands) {
                table.Remove(declarationCommand.getName());
            }
            declaList.RemoveLast();
            foreach (LinkedList<DeclarationCommand> list in declaList) {
                foreach (DeclarationCommand command in list) {
                    if(!table.ContainsKey(command.getName()))
                        table.Add(command.getName(), command.Type);
                }
            }
        }
        
        /**
         * Rebuilds variable set when a Let is removed
         */
//        private void rebuildVariables() {
//            table.Clear();
//            foreach (LinkedList<DeclarationCommand> declarationCommands in declaStack) {
//                foreach (DeclarationCommand declaration in declarationCommands) {
//                    if(!table.ContainsKey(declaration.getName()))
//                        table.Add(declaration.getName(), declaration.Type);
//                }
//            }   
//        }
    }
}