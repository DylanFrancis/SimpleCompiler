using System.Collections;

namespace Project {
    public class Scanner : Compiler{
        private int curPos;
        private readonly string Sentence;

        private readonly ArrayList TokenList = new ArrayList();

        public Scanner(string S) {
            Sentence = S;
            BuildTokenList();
            curPos = 0;
        }

        public void DisplayTokens() {
            for (var x = 0; x <= TokenList.Count - 1; x++)
                ((Token) TokenList[x]).showSpelling();
        }

        public ArrayList getTokens() {
            return TokenList;
        }

        private string BuildNextToken() {
            var Token = "";
            while (Sentence[curPos] == ' ') curPos++;
            while (curPos < Sentence.Length && Sentence[curPos] != ' ') {
                Token = Token + Sentence[curPos];
                curPos++;
            }

            return Token;
        }

        private int FindType(string Spelling) {
            switch (Spelling) {
                case "(": return LPar;
                case ")": return RPar;
                case "+":
                case "-":
                case "*":
                case "/": return Operator;
                case "Let": return Let;
                case "var": 
                case ":":
                case "in":
                case "if":
                case "then":
                case "else":
                default: return Identifier;
            }
        }

        private void BuildTokenList() {
            Token newOne = null;
            while (curPos < Sentence.Length) {
                {
                    var nextToken = BuildNextToken();
                    newOne = new Token(nextToken, FindType(nextToken));
                }
                TokenList.Add(newOne);
            }
        }
    }
}