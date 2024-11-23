using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyLisp.Program;

namespace MyLisp
{
    public enum TokenType
    {
        Identifier,
        OParen,
        CParen,
        Coma,
        Number,
    };
    public class Token
    {
        public TokenType type;
        public string str_value;
        public int int_val;

        public Token(TokenType _type) {
            type = _type;
        }
        public Token(TokenType _type, int _int_val) {
            type = _type;
            int_val = _int_val;
        }
        public Token(TokenType _type, string _str_val) {
            type = _type;
            str_value = _str_val;
        }
        public override string ToString() {
            return type.ToString() + "-" + str_value + "-" + int_val;
        }
    }
    static class Lexer
    {
        
        static public Token[] Tokenize(string text) {
            List<Token> result = new();
            int text_pointer = 0;
            while (text_pointer < text.Length) {
                if (text[text_pointer] == '(') {
                    result.Add(new Token(TokenType.OParen));
                    text_pointer++;
                }
                else if (text[text_pointer] == ')') {
                    result.Add(new Token(TokenType.CParen));
                    text_pointer++;
                }
                else if (text[text_pointer] == ',') {
                    result.Add(new Token(TokenType.Coma));
                    text_pointer++;
                }
                else if (char.IsDigit(text[text_pointer])) {
                    StringBuilder tokenized_number = new StringBuilder();
                    while (text_pointer < text.Length && char.IsDigit(text[text_pointer])) {
                        tokenized_number.Append(text[text_pointer]);
                        text_pointer++;
                    }
                    result.Add(new Token(TokenType.Number, int.Parse(tokenized_number.ToString())));
                }
                else if (char.IsLetter(text[text_pointer])) {
                    StringBuilder tokenized_string = new StringBuilder();
                    while (text_pointer < text.Length && char.IsLetter(text[text_pointer])) {
                        tokenized_string.Append(text[text_pointer]);
                        text_pointer++;
                    }
                    result.Add(new Token(TokenType.Identifier, tokenized_string.ToString()));
                }
                else if (char.IsWhiteSpace(text[text_pointer])) {
                    text_pointer++;
                }
            }
            return result.ToArray();
        }
    }
}
