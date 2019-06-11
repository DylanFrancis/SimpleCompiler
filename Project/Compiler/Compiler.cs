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

        private const int _string = 51;
        private const int _integer = 52;
        
        protected Dictionary<string, int> variables;

        protected Dictionary<string, int> commands;

        protected Dictionary<int, string> syntax;

        protected Compiler() {
            variables = new Dictionary<string, int>();
            variables.Add("string", _string);
            variables.Add("int", _integer);
            
            commands = new Dictionary<string, int>();
            commands.Add("let", _let);
            commands.Add("if", _if);
            
            syntax = new Dictionary<int, string>();
            syntax.Add(_identifier, "identifier");
            syntax.Add(_operator, "operator");
            syntax.Add(_lPar, "(");
            syntax.Add(_rPar, ")");
            syntax.Add(_let, "let");
            syntax.Add(_assign, "=");
            syntax.Add(_if, "if");
            syntax.Add(_var, "var");
            syntax.Add(_colon, ":");
            syntax.Add(_in, "int");
            syntax.Add(_then, "then");
            syntax.Add(_else, "else");
            syntax.Add(_end, "end");
            syntax.Add(_condition, "condition");
            syntax.Add(_string, "string");
            syntax.Add(_integer, "integer");
//            commands.Add("in", _in);
//            commands.Add("then", _then);
//            commands.Add("else", _else);
//            commands.Add("=", _assign);
        }

        protected string getName(int type) {
            return syntax[type];
        }

        protected bool variableExists(string check) {
            return variables.ContainsKey(check);
        }

        protected bool commandExists(string check) {
            return commands.ContainsKey(check);
        }
    }
}