using System;
using System.Collections;

namespace Project {
    public class Scanner {
        const int Identifier = 1; 
        const int Operator = 2; 
        const int LPar = 3; 
        const int RPar = 4;

        ArrayList TokenList = new ArrayList();
        String Sentence;     
        int curPos;

        public Scanner(String S){   
            Sentence = S;
            BuildTokenList();
            curPos = 0;
        }

        public void DisplayTokens(){   
            for (int x = 0; x <= TokenList.Count - 1; x++)
                ((Token)TokenList[x]).showSpelling();
        }

        public ArrayList getTokens(){ return TokenList; }

        String BuildNextToken(){   
            String Token = "";
            while (Sentence[curPos] == ' ')   curPos++;
            while ((curPos < Sentence.Length) && (Sentence[curPos] != ' '))  
            {   
                Token = Token + Sentence[curPos];
                curPos++;
            }
            return Token;
        }

        int FindType(String Spelling){
            switch (Spelling) {
                case "(": return LPar;
                case ")": return RPar;
                case "+": 
                case "-":
                case "*":
                case "/": return Operator;
                case "Let":
                case "var":
                case ":":
                case "in":
                case "if":
                case "then":
                case "else":
                default: return Identifier;
            }
        }
                    
        void BuildTokenList(){   
            Token newOne = null;
            while (curPos < Sentence.Length)
            {
                {   
                    String nextToken = BuildNextToken();
                    newOne = new Token(nextToken, FindType(nextToken));
                }
                TokenList.Add(newOne);
            }
        }

    }
}