using System.Collections.Generic;

namespace Project {
    public class CompilerUtils : Compiler {
        protected Dictionary<string, int> types;
        protected Dictionary<string, int> commands;
        protected Dictionary<int, string> syntax;

        private CompilerUtils() {
            types = new Dictionary<string, int>();
            types.Add("string", _string);
            types.Add("int", _integer);
            types.Add("boolean", _boolean);
            
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
            syntax.Add(_boolean, "boolean");
            syntax.Add(_conditionOp, "condition operator");
            syntax.Add(_literalString, "literal string");
            syntax.Add(_literalInt, "literal int");
//            commands.Add("in", _in);
//            commands.Add("then", _then);
//            commands.Add("else", _else);
//            commands.Add("=", _assign);
        }

        public Dictionary<string, int> Types => types;

        public Dictionary<string, int> Commands => commands;

        public Dictionary<int, string> Syntax => syntax;

        public static CompilerUtils getInstance() {
            if (_compilerUtils == null) {
                _compilerUtils = new CompilerUtils();
            }
            return _compilerUtils;
        }
    }
}