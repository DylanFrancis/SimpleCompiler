using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Project.AbstractSyntaxTree.NonTerminals;
using Project.NonTerminals;

namespace Project {
    public class Parser : Compiler{
        
        private Token CurrentToken;
        private int CurTokenPos;
        private int prevLine = -1;
        private readonly ArrayList TokenList;
        private LetCommand root;

        public Parser(LinkedList<Line> code) {
            _compilerUtils = CompilerUtils.getInstance();
            Scanner scan = new Scanner(code);
            TokenList = scan.getTokens();                
            parse();
        }

        public int parse() {
            CurTokenPos = -1;
            FetchNextToken(); 
            parseLet();
            return errorCount;
        }

        public Parser(List<string> lines) { }

        public LetCommand getRoot() {
            return root;
        }

        private LetCommand parseLet() {
            if (CurrentToken != null && accept(_let)) {
                LetCommand curRoot = new LetCommand(prevLine);
                
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
            logError(CurrentToken, "Syntax error: Let required.");
            return null;
        }

        private DeclarationCommand parseDeclaration() {
            if (accept(_var)) {
                
                DeclarationCommand declarationCommand = new DeclarationCommand(prevLine, new Identifier(prevLine, CurrentToken.getSpelling()));
                declarationCommand.Line = CurrentToken.Line;
                accept(_identifier); // 
                
                if (!accept(_colon)) {
                    string[] msg = {"Syntax Error: variable declaration", "Given: " + CurrentToken.getSpelling() + "\"" , "Expected: " + ":" + "\""};
                    logError(CurrentToken, msg);
                    return null;
                }
                
                if (typeExists(CurrentToken.getSpelling())) {
                    declarationCommand.Type = CurrentToken.getSpelling();
                    nextToken();
                    return declarationCommand;
                }
                logError(CurrentToken, "Syntax Error: unknown variable: " + CurrentToken.getSpelling());
                nextToken();
                return null;
            }
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
            ConditionExpression expression = parseConditionExpression();
            
            IfCommand ifCommand = new IfCommand(CurrentToken.Line, expression);
            
            if (accept(_then)) {
                Command thenCommand = parseCommand();
                
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
            AssignCommand assignCommand = new AssignCommand(CurrentToken.Line, new IdentifierPE(CurrentToken.Line, parseIdentifier()));
            
            if (accept(_assign)) {
                if (Peek() == _operator) {
                    assignCommand.Expression = parseExpression();
                }
                else {
                    switch (CurrentToken.getType()) {
                        case _condition: assignCommand.AssignTo = new ConditionPE(CurrentToken.Line, parseCondition());
                            break;
                        case _identifier: assignCommand.AssignTo = new IdentifierPE(CurrentToken.Line, parseIdentifier());
                            break;
                        case _literalString: assignCommand.AssignTo = new LiteralString(CurrentToken.Line, parseLiteral());
                            break;
                        case _literalInt: assignCommand.AssignTo = new LiteralInt(CurrentToken.Line, parseLiteral());
                            break;
                    }   
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
            prevLine = CurrentToken.Line;
            if (CurrentToken.matchesType(Type)){
                FetchNextToken();
                return true;
            }
            string[] msg = {"Given: \"" + CurrentToken.getSpelling() + "\"", "Expected: \"" + getName(Type) + "\""};
            logError(CurrentToken, msg);
            FetchNextToken();
            return false;
        }
        
        private void nextToken() {
            FetchNextToken();
        }

        private ConditionExpression parseConditionExpression() {
            ConditionExpression conditionExpression;
            AST P1, P2;
            if (Peek() == _operator) P1 = parseExpression();
            else P1 = parsePrimary();

            var C = parseConditionOperator();

            if (Peek() == _operator) P2 = parseExpression();
            else P2 = parsePrimary();

            conditionExpression = new ConditionExpression(prevLine, P1, C, P2);
            return conditionExpression;
        }

        private Expression parseExpression() {
            Expression ExpAST;
            var P1 = parsePrimary();
            var O = parseOperator();
            var P2 = parsePrimary();
            ExpAST = new Expression(prevLine, P1, O, P2); 
            return ExpAST;
        }

        private PrimaryExpression parsePrimary() {
            PrimaryExpression PE;
            if (CurrentToken == null)
                return null;
            switch (CurrentToken.getType()) {
//                case _condition:
                case _identifier:
                    var Id = parseIdentifier();
                    PE = new IdentifierPE(prevLine, Id);
                    break;
                case _lPar:
                    nextToken();
                    PE = new BracketsPE(prevLine, parseExpression());
                    accept(_rPar);
                    break;
                case _condition:
                    var C = parseCondition();
                    PE = new ConditionPE(prevLine, C);
                    break;
                case _literalString:
                    var S = parseLiteral();
                    PE = new LiteralString(prevLine, S);
                    break;
                case _literalInt:
                    var I = parseLiteral();
                    PE = new LiteralString(prevLine, I);
                    break;
                default:
                    logError(CurrentToken, "Syntax Error in Primary");
                    PE = null;
                    break;
            }
            return PE;
        }

        private Literal parseLiteral() {
            var L = new Literal(CurrentToken.Line, CurrentToken.getSpelling());
            if (CurrentToken.getType() == _literalInt){
                accept(_literalInt);
                return L;
            }

            if (CurrentToken.getType() == _literalString) {
                accept(_literalString);
                return L;
            }

            accept(_literalString);
            return L;
        }

        private Condition parseConditionOperator() {
            var C = new Condition(CurrentToken.Line, CurrentToken.getSpelling());
            accept(_conditionOp);
            return C;
        }
        
        private Condition parseCondition() {
            var C = new Condition(CurrentToken.Line, CurrentToken.getSpelling());
            accept(_condition);
            return C;
        }

        private Identifier parseIdentifier() {
            var I = new Identifier(CurrentToken.Line, CurrentToken.getSpelling());
            accept(_identifier);
            return I;
        }

        private Operator parseOperator() {
            var op = new Operator(CurrentToken.Line, CurrentToken.getSpelling());
            accept(_operator);
            return op;
        }
    }
}