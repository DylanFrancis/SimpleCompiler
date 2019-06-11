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
        private LetCommand root;
        private LetCommand curRoot;

        public Parser(string Sentence) : base() {            
            Scanner scan = new Scanner(Sentence);
            TokenList = scan.getTokens();
            parse();
//            var P = parseExpression();
        }

        public Parser(LinkedList<string> code) : base() {
            Scanner scan;
            String tokens = "";
            foreach (string s in code) {
                tokens += s;
                tokens += " ";
            }
            tokens = tokens.Trim();
            scan = new Scanner(tokens);
            TokenList = scan.getTokens();                
            parse();
        }

        private void parse() {
            CurTokenPos = -1;
            FetchNextToken(); 
            parseLet();
        }

        public Parser(List<string> lines) { }

        public LetCommand getRoot() {
            return root;
        }

        private LetCommand parseLet() {
            if (CurrentToken != null && accept(_let)) {
                curRoot = new LetCommand();
                if (root == null) root = curRoot;
                
               
                while (CurrentToken.getType() != _in) {
                    // accept(_in);
                    curRoot.addDeclaration(parseDeclaration());
                }

                accept(_in);
                while (CurrentToken.getType() != _end) {
                    curRoot.addCommand(parseCommand());
                }

                accept(_end);
                return curRoot;
            }
            else {
                Console.WriteLine("Syntax error: Let required.");
                return null;
            }
        }

        private DeclarationCommand parseDeclaration() {
            if (accept(_var)) {
                
                DeclarationCommand declarationCommand = new DeclarationCommand(new Identifier(CurrentToken.getSpelling()));
                accept(_identifier); // 
                
                if (!accept(_colon)) {
                    Console.WriteLine("Syntax Error: variable declaration");
                    Console.WriteLine("    Given: " + CurrentToken.getSpelling() + "\"");
                    Console.WriteLine("    Expected: " + ":" + "\"");
                    return null;
                }
                
                if (variableExists(CurrentToken.getSpelling())) {
                    declarationCommand.Type = CurrentToken.getSpelling();
                    nextToken();
                    return declarationCommand;
                }
                             
                Console.WriteLine("Syntax Error: unknown variable: " + CurrentToken.getSpelling());
                nextToken();
                return null;
            }
            Console.WriteLine("Syntax Error: variable declaration");
            return null;
        }

        private Command parseCommand() {
//            if (commandExists(CurrentToken.getSpelling())) {
                Command command = null;
                switch (CurrentToken.getType()) {
                    case _let: 
                        command = parseLet();
                      break;
                    case _if:
                        command = parseIf();
                        break;
                    default:
                        command = parseAssign();
                        break;
                }
                return command;
//            }
//            return null;
        }

        private IfCommand parseIf() {
            accept(_if);
            Expression expression = parseExpression();
            
            IfCommand ifCommand = new IfCommand(expression);
            //nextToken();
            
            if (accept(_then)) {
                Command thenCommand = parseCommand();
                
//                nextToken();
                ifCommand.ThenCommand = thenCommand;
                
                if (accept(_else)) {
                    Command elseCommand = parseCommand();
                    ifCommand.ElseCommand = elseCommand;
                    return ifCommand;
                }
            }
            return null;
        }

        private AssignCommand parseAssign() {
            accept(_identifier);
            AssignCommand assignCommand = new AssignCommand(new Identifier(CurrentToken.getSpelling()));
//            nextToken();
            
            if (accept(_assign)) {
                if (Peek() == _operator) {
                    assignCommand.Expression = parseExpression();
                }
                else {
                    if (CurrentToken.getType() == _condition) assignCommand.Assign = parseCondition();
                    else assignCommand.Assign = parseIdentifier();
                }
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

        private int Peek() {
            return ((Token)TokenList[CurTokenPos + 1]).getType();
        }

        private Boolean accept(int Type) {
            if (CurrentToken.matchesType(Type)){
                FetchNextToken();
                return true;
            }
            Console.WriteLine("Syntax Error: Type mismatch");
            Console.WriteLine("    Given: \"" + CurrentToken.getType() +"\" - \"" + CurrentToken.getSpelling() + "\"");
            Console.WriteLine("    Expected: \"" + Type +"\" - \"" + getName(Type) + "\"");
            return false;
        }
        
        private void nextToken() {
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
                    nextToken();
                    PE = new BracketsPE(parseExpression());
                    accept(_rPar);
                    break;
                case _condition:
                    var C = parseCondition();
                    PE = new ConditionPE(C);
                    break;
                default:
                    Console.WriteLine("Syntax Error in Primary");
                    PE = null;
                    break;
            }
            return PE;
        }

        private Condition parseCondition() {
            var C = new Condition(CurrentToken.getSpelling());
            accept(_condition);
            return C;
        }

        private Identifier parseIdentifier() {
            var I = new Identifier(CurrentToken.getSpelling());
            accept(_identifier);
            return I;
        }

        private Operator parseOperator() {
            var op = new Operator(CurrentToken.getSpelling());
            accept(_operator);
            return op;
        }
    }
}