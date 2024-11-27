using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        public string variable_name;
        public int value;
        public List<Expr> expressions = new();

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
        ];
        static public bool ExpectKind(TokenType expexted) {
            if (tokens[++tp].type == expexted) return true;
            else return false;

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
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "print":
                    expr = new Expr(ExprType.FunctionCall, "print");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "pass":
                    expr = new Expr(ExprType.FunctionCall, "pass");
                    if (ExpectKind(TokenType.OParen)) { }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                    break;
                case "assign":
                    expr = new Expr(ExprType.FunctionCall, "assign");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "sub":
                    expr = new Expr(ExprType.FunctionCall, "sub");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "mul":
                    expr = new Expr(ExprType.FunctionCall, "mul");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "div":
                    expr = new Expr(ExprType.FunctionCall, "div");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "cmp":
                    expr = new Expr(ExprType.FunctionCall, "cmp");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "ls":
                    expr = new Expr(ExprType.FunctionCall, "ls");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "gr":
                    expr = new Expr(ExprType.FunctionCall, "gr");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "loop":
                    expr = new Expr(ExprType.FunctionCall, "loop");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "var":
                    expr = new Expr(ExprType.FunctionCall, "var");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "mod":
                    expr = new Expr(ExprType.FunctionCall, "mod");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                case "if":
                    expr = new Expr(ExprType.FunctionCall, "if");
                    if (ExpectKind(TokenType.OParen)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.OParen}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.Coma)) {
                        tp++;
                        ParseArgs(expr);
                    }
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.Coma}");
                    if (ExpectKind(TokenType.CParen)) break;
                    else throw new Exception($"Unexpexted token: {tokens[tp - 1].type}, expected {TokenType.CParen}");
                default:
                    throw new Exception($"Unknown identifier: \'{tokens[tp].str_value}\'");

            }
            return expr;
        }
    }
}
