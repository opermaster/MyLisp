using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq.Expressions;
using System.Net;
namespace MyLisp
{
    class Program
    {
        public static void Main() {
            string text = File.ReadAllText("./Program.txt");
            Console.WriteLine(text);
            Parser.tokens = Lexer.Tokenize(text);
            Console.WriteLine("result:\n");
            Expr[] program = Parser.ParseTokenArr();
            foreach (Expr ex in program) {
                Evaluator.EvalExpr(ex);
            }

        }
    }
}
