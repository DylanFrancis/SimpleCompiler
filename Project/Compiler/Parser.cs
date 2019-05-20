using System;
using System.Collections;
using System.Collections.Generic;

namespace Project {
    public class Parser : Compiler{
        
        private Token CurrentToken;
        private int CurTokenPos;
        private readonly ArrayList TokenList;

        public Parser(string Sentence) {
            var S = new Scanner(Sentence);

            TokenList = S.getTokens();
            CurTokenPos = -1;
            FetchNextToken();
            var P = parseExpression();
        }

        public Parser(List<string> lines) { }

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
                case Identifier:
                    var I = parseIdentifier();
                    PE = new IdentifierPE(I);
                    break;
                case LPar:
                    acceptIt();
                    PE = new BracketsPE(parseExpression());
                    accept(RPar);
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
            accept(Identifier);
            return I;
        }

        private Operator parseOperator() {
            var O = new Operator(CurrentToken.getSpelling());
            accept(Operator);
            return O;
        }
    }
}