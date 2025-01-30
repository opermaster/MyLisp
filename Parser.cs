using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLisp
{
    public enum ExprType
    {
        FunctionCall,
        Variable,
        Number,
        Block,
    }
    public class Expr
    {
        public ExprType type;
        public string function_name;
        public string _function_name;
        public string variable_name;
        public int value;
        public int args_count;
        public List<Expr> expressions = new();
        public List<Expr> _variables = new();

        public Expr() { }
        public Expr(ExprType _type, params Expr[] _expressions) {
            type = _type;
            expressions.AddRange(_expressions);
        }
        public Expr(ExprType _type, int _value) {
            type = _type;
            value = _value;
        }
        public Expr(ExprType _type) {
            type = _type;
        }
        public Expr(ExprType _type, string _function_name) {
            type = _type;
            function_name = _function_name;
        }
        public Expr(ExprType _type, string _variable_name, int _value) {
            type = _type;
            variable_name = _variable_name;
            value = _value;
        }
        public Expr(ExprType _type, string _function_name, params Expr[] _expressions) {
            type = _type;
            function_name = _function_name;
            expressions.AddRange(_expressions);
        }
    }
    class Parser
    {
        static public Token[] tokens;
        static public int tp = 0;
        static private string[] key_words = [
            "add",
            "print",
            "sub",
            "cmp",
            "div",
            "mul",
            "var",
            "if",
            "assign",
            "loop",
            "ls",
            "gr",
            "mod",
            "pass",
            "proc",
        ];
        static private List<(string, int)> users_declared_functions = new();
        static public void ExpectKind(TokenType expected) {
            if (tokens[++tp].type != expected) {
                throw new Exception($"Unexpexted token: {tokens[tp-1].type}, expected {expected}");
            }
        }
        static public Expr[] ParseBlock() {
            List<Expr> block = new List<Expr>();
            do {
                block.Add(ParseExpression());
                tp++;
            } while (tp < tokens.Length && tokens[tp].type != TokenType.CCParen);
            return block.ToArray();
        }
        static public Expr ParseVariable() {
            return new Expr(ExprType.Variable, tokens[tp].str_value,0);
        }
        static public Expr ParseNumber() {
            return new Expr(ExprType.Number, tokens[tp].int_val);
        }
        static private void ParseArgs(Expr expression) {
            switch (tokens[tp].type) {
                case TokenType.COParen:
                    tp++;
                    expression.expressions.Add(new Expr(ExprType.Block, ParseBlock()));
                    break;
                case TokenType.Number:
                    expression.expressions.Add(ParseNumber());
                    break;
                case TokenType.Identifier:
                    if (key_words.Contains(tokens[tp].str_value)) {
                        expression.expressions.Add(ParseExpression());
                        break;
                    } else {
                        expression.expressions.Add(ParseVariable());
                        break;
                    }
            }
        }
        static public Expr[] ParseTokenArr() {
            List<Expr> exps = new List<Expr>();
            while (tp < tokens.Length) {
                switch (tokens[tp].type) {
                    case TokenType.COParen:
                        List<Expr> block = new List<Expr>();
                        tp++;
                        block.AddRange(ParseTokenArr());
                        exps.Add(new Expr(ExprType.Block,block.ToArray()));
                        break;
                    case TokenType.Identifier:
                        exps.Add(ParseExpression());
                        break;
                }
                tp++;
            }
            return exps.ToArray();
        }
        static public Expr ParseExpression() {
            Expr expr = new Expr();
            switch (tokens[tp].str_value) {
                case "add":
                    expr = new Expr(ExprType.FunctionCall, "add");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;

                case "print":
                    expr = new Expr(ExprType.FunctionCall, "print");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "pass":
                    expr = new Expr(ExprType.FunctionCall, "pass");
                    ExpectKind(TokenType.OParen);
                    ExpectKind(TokenType.CParen);
                    break;
                case "assign":
                    expr = new Expr(ExprType.FunctionCall, "assign");
                    ExpectKind(TokenType.OParen);
                        tp++;
                        ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                        tp++;
                        ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "sub":
                    expr = new Expr(ExprType.FunctionCall, "sub");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "mul":
                    expr = new Expr(ExprType.FunctionCall, "mul");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "div":
                    expr = new Expr(ExprType.FunctionCall, "div");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "cmp":
                    expr = new Expr(ExprType.FunctionCall, "cmp");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "ls":
                    expr = new Expr(ExprType.FunctionCall, "ls");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "gr":
                    expr = new Expr(ExprType.FunctionCall, "gr");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "loop":
                    expr = new Expr(ExprType.FunctionCall, "loop");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "var":
                    expr = new Expr(ExprType.FunctionCall, "var");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "mod":
                    expr = new Expr(ExprType.FunctionCall, "mod");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "if":
                    expr = new Expr(ExprType.FunctionCall, "if");
                    ExpectKind(TokenType.OParen);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.Coma);
                    tp++;
                    ParseArgs(expr);
                    ExpectKind(TokenType.CParen);
                    break;
                case "proc":
                    expr = new Expr(ExprType.FunctionCall, "proc");
                    ExpectKind(TokenType.OParen);
                    ExpectKind(TokenType.Identifier);
                    expr._function_name = tokens[tp].str_value;
                    tp++;
                    ExpectKind(TokenType.Number);
                    expr.args_count = tokens[tp].int_val;
                    users_declared_functions.Add((expr._function_name, expr.args_count));
                    for (int i = 0; i <= expr.args_count; i++) {
                        ExpectKind(TokenType.Coma);
                        tp++;
                        ParseArgs(expr);
                    }
                    ExpectKind(TokenType.CParen);
                    break;
                default:
                    bool is_declared = false;
                    int args_count = 0;
                    foreach(var item in users_declared_functions) {
                        if (item.Item1 == tokens[tp].str_value) {
                            args_count = item.Item2;
                            is_declared = true;
                            break;
                        }
                    }
                    if (is_declared) {
                        expr = new Expr(ExprType.FunctionCall, tokens[tp].str_value);
                        ExpectKind(TokenType.OParen);
                        for (int i = 0; i < args_count; i++) {
                            ExpectKind(TokenType.Coma);
                            tp++;
                            ParseArgs(expr);
                        };
                        ExpectKind(TokenType.CParen);
                        break;
                    }
                    else throw new Exception($"Unknown identifier: \'{tokens[tp].str_value}\'");

            }
            return expr;
        }
    }
}
