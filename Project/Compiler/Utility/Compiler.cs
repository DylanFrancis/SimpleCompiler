using System;
using System.Collections.Generic;

namespace Project
{
    public abstract class Compiler{
        protected const int _identifier = 1;
        protected const int _operator = 2;
        protected const int _lPar = 3;
        protected const int _rPar = 4;
        protected const int _let = 5;
        protected const int _assign = 6;
        protected const int _if = 7;
        protected const int _var = 8;
        protected const int _colon = 9;
        protected const int _in = 10;
        protected const int _then = 11;
        protected const int _else = 12;
        protected const int _end = 13;
//        protected const int _assign = 14;
        protected const int _condition = 14;
        protected const int _conditionOp = 15;
        protected const int _literalString = 16;
        protected const int _literalInt = 17;

        protected const int _string = 51;
        protected const int _integer = 52;
        protected const int _boolean = 53;
        protected static CompilerUtils _compilerUtils;

        protected string getName(int type) {
            return _compilerUtils.Syntax[type];
        }

        protected bool typeExists(string check) {
            return _compilerUtils.Types.ContainsKey(check);
        }

        protected bool commandExists(string check) {
            return _compilerUtils.Commands.ContainsKey(check);
        }
        
        
//        private string getConditionType(Condition c) {
//            switch (c.Name) {
//                case "<":        
//                case ">":    
//                case ">=":    
//                case "<=":
//                case "==":    
//                case "!=":    return _conditionOp;
//            }
//        }

        protected void logError(Token token, string msg) {
            Console.WriteLine("Error at line: " + token.Line);
            Console.WriteLine("    " + msg);
            Console.WriteLine();
        }

        protected void logError(Token token, string[] msg) {
            Console.WriteLine("Error at line: " + token.Line);
            foreach (string s in msg) {
                Console.WriteLine("    " + s);
            }
            Console.WriteLine();
        }

        protected void logCError(AST ast, string[] msg) {
            Console.WriteLine("Contextual Error at line: " + ast.Line);
            foreach (string s in msg) {
                Console.WriteLine("    " + s);
            }
            Console.WriteLine();
        }
        protected void logCError(AST ast, string msg) {
            Console.WriteLine("Contextual Error at line: " + ast.Line);
            Console.WriteLine("    " + msg);
            Console.WriteLine();
        }
    }
}