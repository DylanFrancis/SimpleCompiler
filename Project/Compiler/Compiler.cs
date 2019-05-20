using System.Collections.Generic;

namespace Project
{
    public abstract class Compiler
    {
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
//        protected const int _assign = 13;

        private const int _string = 1;
        private const int _integer = 2;
        
        protected Dictionary<string, int> variables;

        protected Dictionary<string, int> commands;

        protected Compiler() {
            variables = new Dictionary<string, int>();
            variables.Add("string", _string);
            variables.Add("int", _integer);
            
            commands = new Dictionary<string, int>();
            commands.Add("let", _let);
            commands.Add("if", _if);
            commands.Add("in", _in);
            commands.Add("then", _then);
            commands.Add("else", _else);
//            commands.Add("=", _assign);
        }

        protected bool variableExists(string check) {
            return variables.ContainsKey(check);
        }

        protected bool commandExists(string check) {
            return commands.ContainsKey(check);
        }
    }
}