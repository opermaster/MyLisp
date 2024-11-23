using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLisp
{
    class Evaluator
    {
        static public int EvalFuncCall(Expr program) {
            int left, right;
            int body;
            int condition;
            switch (program.function_name) {
                case "add":
                    left = EvalExpr(program.expressions[0]);
                    right = EvalExpr(program.expressions[1]);
                    return left + right;
                case "mul":
                    left = EvalExpr(program.expressions[0]);
                    right = EvalExpr(program.expressions[1]);
                    return left * right;
                case "sub":
                    left = EvalExpr(program.expressions[0]);
                    right = EvalExpr(program.expressions[1]);
                    return left - right;
                case "div":
                    left = EvalExpr(program.expressions[0]);
                    right = EvalExpr(program.expressions[1]);
                    return left / right;
                case "cmp":
                    left = EvalExpr(program.expressions[0]);
                    right = EvalExpr(program.expressions[1]);
                    return left == right ? 1 : 0;
                case "if":
                    condition = EvalExpr(program.expressions[0]);
                    if (condition == 1) return EvalExpr(program.expressions[1]);
                    else return EvalExpr(program.expressions[2]);
                case "print":
                    Console.WriteLine(EvalExpr(program.expressions[0]));
                    return 0;
                default:
                    return -1;
            };
        }
        static public int EvalExpr(Expr program) { 
            switch (program.type) {
                case ExprType.FunctionCall:
                    return EvalFuncCall(program);
                case ExprType.Number:
                    return program.value;
                default:
                    return -1;
            };
        }
    }
}
