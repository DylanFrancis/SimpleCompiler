using System;
using System.Collections;
using System.Collections.Generic;
using Project.AbstractSyntaxTree.NonTerminals;
using Project.NonTerminals;

namespace Project {
    public class Parser : Compiler{
        
        private Token CurrentToken;
        private int CurTokenPos;
        private readonly ArrayList TokenList;

        public Parser(string Sentence) : base() {
            
            var S = new Scanner(Sentence);

            TokenList = S.getTokens();
            CurTokenPos = -1;
            FetchNextToken();
            
//            var P = parseExpression();
        }

        public Parser(List<string> lines) { }


        private LetCommand parseLet() {
            if (CurrentToken == null)
                return null;
            if (CurrentToken.getType() == _let) {
                LetCommand root = new LetCommand();
                acceptIt();
                while (CurrentToken.getSpelling() != "in") {
                    root.addDeclaration(parseDeclaration());
                }
                acceptIt();
                while (CurrentToken.getSpelling() != "end") {
                    root.addCommand(parseCommand());
                }
                
                return root;
            }

            return null;

        }

        private DeclarationCommand parseDeclaration() {
            if (CurrentToken.getType() == _var) {
                DeclarationCommand declarationCommand = new DeclarationCommand(new Identifier(CurrentToken.getSpelling()));
                acceptIt();
                if (CurrentToken == null || CurrentToken.getType() != _colon) {
                    Console.WriteLine("Syntax Error: variable declaration");
                    return null;
                }
                acceptIt();
                if (variableExists(CurrentToken.getSpelling())) {
                    declarationCommand.Type = CurrentToken.getSpelling();
                    acceptIt();
                    return declarationCommand;
                }
                Console.WriteLine("Syntax Error: unknown variable");
                return null;
            }
            Console.WriteLine("Syntax Error: variable declaration");
            return null;
        }

        private Command parseCommand() {
            if (commandExists(CurrentToken.getSpelling())) {
                Command command = null;
                switch (CurrentToken.getType()) {
                    case _let: 
                        command = parseLet();
                      break;
                    case _if:
                        acceptIt();
                        command = parseIf();
                        break;
                    default:
                        command = parseAssign();
                        break;
                }
                return command;
            }
            return null;
        }

        private IfCommand parseIf() {
            Expression expression = parseExpression();
            IfCommand ifCommand = new IfCommand(expression);
            acceptIt();
            if (CurrentToken.getType() == _then) {
                acceptIt();
                Command thenCommand = parseCommand();
                acceptIt();
                ifCommand.ThenCommand = thenCommand;
                if (CurrentToken.getType() == _else) {
                    acceptIt();
                    Command elseCommand = parseCommand();
                    ifCommand.ElseCommand = elseCommand;
                    return ifCommand;
                }
            }
           
            
            return null;
        }

        private AssignCommand parseAssign() {
            AssignCommand assignCommand = new AssignCommand(new Identifier(CurrentToken.getSpelling()));
            acceptIt();
            if (CurrentToken.getType() == _assign) {
                acceptIt();
                Expression expression = parseExpression();
                assignCommand.Expression = expression;
                return assignCommand;
            }

            return null;
        }
        
        
        
        private void FetchNextToken() {
            CurTokenPos++;
            if (CurTokenPos < TokenList.Count)
                CurrentToken = (Token) TokenList[CurTokenPos];
            else
                CurrentToken = null;
        }

        private void accept(int Type) {
            if (CurrentToken.matchesType(Type))
                FetchNextToken();
            else
                Console.WriteLine("Syntax Error in accept");
        }

        /**
         * Sets CurrentToken to next token or null if last token is complete.
         * Increments CurTokenPos
         */
        private void acceptIt() {
            FetchNextToken();
        }

        private Expression parseExpression() {
            Expression ExpAST;
            var P1 = parsePrimary();
            var O = parseOperator();
            var P2 = parsePrimary();
            ExpAST = new Expression(P1, O, P2); 
            return ExpAST;
        }

        private PrimaryExpression parsePrimary() {
            PrimaryExpression PE;
            if (CurrentToken == null)
                return null;
            switch (CurrentToken.getType()) {
                case _identifier:
                    var I = parseIdentifier();
                    PE = new IdentifierPE(I);
                    break;
                case _lPar:
                    acceptIt();
                    PE = new BracketsPE(parseExpression());
                    accept(_rPar);
                    break;
                default:
                    Console.WriteLine("Syntax Error in Primary");
                    PE = null;
                    break;
            }

            return PE;
        }

        private Identifier parseIdentifier() {
            var I = new Identifier(CurrentToken.getSpelling());
            accept(_identifier);
            return I;
        }

        private Operator parseOperator() {
            var O = new Operator(CurrentToken.getSpelling());
            accept(_operator);
            return O;
        }
    }
}