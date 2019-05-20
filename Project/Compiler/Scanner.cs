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
                case ":=":    return _assign;
                default:      return _identifier;
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