using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Project {
    public class Scanner : Compiler{
//        private int curPos;
//        private readonly string Sentence;

        private readonly ArrayList TokenList = new ArrayList();
        private readonly LinkedList<Line> SentenceList;
        
//        private readonly LinkedList<Token> TokenList = new LinkedList<Token>();
        
//        public Scanner(string S) {
//            Sentence = S;
//            BuildTokenList();
//            curPos = 0;
//        }
        public Scanner(LinkedList<Line> list) {
            SentenceList = list;
            BuildTokenList();
        }
        

        public void DisplayTokens() {
//            LinkedList<Token>.Enumerator enumerator = TokenList.GetEnumerator();
//            while (enumerator.MoveNext()){
//                enumerator.Current.showSpelling();
//            }
            for (var x = 0; x <= TokenList.Count - 1; x++)
                ((Token) TokenList[x]).showSpelling();
        }

        public ArrayList getTokens() {
            return TokenList;
        }
        
        private void BuildTokenList() {
            Token newOne = null;
            LinkedList<Line>.Enumerator enumerator = SentenceList.GetEnumerator();
            while (enumerator.MoveNext()) {

                string line = enumerator.Current.Code;
                line = line.Trim();
                int cur = 0;
                var Token = "";

                while (cur < line.Length) {
                    Token = "";
                    while (line[cur] == ' ') cur++;
                    while (cur < line.Length && line[cur] != ' ') {
                        Token += line[cur];
                        cur++;
                    }
                    newOne = new Token(Token, FindType(Token), enumerator.Current.Line1);
                    TokenList.Add(newOne);
                }
                
            }
        }
        
        private string BuildNextToken(string Sentence) {
            var Token = "";
            int curPos = 0;
            while (Sentence[curPos] == ' ') curPos++;
            while (curPos < Sentence.Length && Sentence[curPos] != ' ') {
                Token = Token + Sentence[curPos];
                curPos++;
            }

            Token = Token.Trim();
            return Token;
        }

//        private string BuildNextToken() {
//            var Token = "";
//            while (Sentence[curPos] == ' ') curPos++;
//            while (curPos < Sentence.Length && Sentence[curPos] != ' ') {
//                Token = Token + Sentence[curPos];
//                curPos++;
//            }
//
//            Token = Token.Trim();
//            return Token;
//        }

        private int FindType(string Spelling) {
            switch (Spelling) {
                case "(":     return _lPar;
                case ")":     return _rPar;
                case "+":
                case "-":
                case "*":
                case "/":     return _operator;
                case "Let":   return _let;
                case "if":    return _if;
                case "var":   return _var;
                case ":":     return _colon;
                case "in":    return _in;
                case "then":  return _then;
                case "else":  return _else;
                case "=":     return _assign;
                case "true":  
                case "false": return _condition;
                case "<":    
                case ">":    
                case ">=":    
                case "<=":
                case "==":    
                case "!=":    return _conditionOp;
                case "end":   return _end;
                default:
                    int i;
                    if (Int32.TryParse(Spelling, out i)) {
                        return _literalInt;
                    }

                    if (Spelling[0] == '\"' && Spelling[Spelling.Length - 1] == '\"') {
                        return _literalString;
                    }
                    return _identifier;
            }
        }
    }
}