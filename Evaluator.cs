using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLisp
{
    class Evaluator
    {
        static private Dictionary<string, int> variables = new Dictionary<string, int>();
        static public int EvalFuncCall(Expr program) {
            int left, right;
            int body;
            int condition;
            int var_val;
            int var_new_val;
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
                case "var":
                    variables[program.expressions[0].variable_name] = 0;
                    body = EvalExpr(program.expressions[0]);
                    return body;
                case "assign":
                    variables[program.expressions[0].variable_name]= EvalExpr(program.expressions[1]);
                    return EvalExpr(program.expressions[0]);
                case "loop":
                    condition = EvalExpr(program.expressions[0]);
                    return EvalExpr(program.expressions[0]);
                case "print":
                    Console.WriteLine(EvalExpr(program.expressions[0]));
                    return 0;
                default:
                    return -1;
            };
        }
        static public int EvalExpr(Expr program) { 
            switch (program.type) {
                case ExprType.Block:
                    int res = 0;
                    foreach (Expr block_expr in program.expressions) 
                        res+=EvalExpr(block_expr);
                    return res;
                case ExprType.FunctionCall:
                    return EvalFuncCall(program);
                case ExprType.Number:
                    return program.value;
                case ExprType.Variable:
                    int out_value_var;
                    if (variables.TryGetValue(program.variable_name, out out_value_var)) {
                        return out_value_var;
                    }
                    else throw new Exception($"Using of not identified variable: \'{program.variable_name}\'");
                    
                default:
                    return -1;
            };
        }
    }
}
