using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLisp
{
    public enum ExprType
    {
        FunctionCall,
        Add,
        Sub,
        Mul,
        Number,
    }
    public class Expr
    {
        

        public ExprType type;
        public string function_name;
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
        static public bool ExpectKind(TokenType expexted) {
            if (tokens[++tp].type == expexted) return true;
            else return false;

        }
        static public Expr ParseNumber() {
            return new Expr(ExprType.Number, tokens[tp].int_val);
        }
        static private void ParseArgs(Expr expression) {
            switch (tokens[tp].type) {
                case TokenType.Number:
                    expression.expressions.Add(ParseNumber());
                    break;
                case TokenType.Identifier:
                    expression.expressions.Add(ParseExpression());
                    break;
            }
        }
        static public Expr[] ParseTokenArr() {
            List<Expr> exps = new List<Expr>();
            while (tp < tokens.Length) {
                switch (tokens[tp].type) {
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
                    throw new Exception($"Unknown identifier: {tokens[tp].str_value}");

            }
            return expr;
        }
    }
}
