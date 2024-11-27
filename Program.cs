using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq.Expressions;
using System.Net;
using System.Drawing;
using System.Reflection;
namespace MyLisp
{
    class Compiler
    {
        static public StringBuilder output_program = new StringBuilder();
        static private int instruction_p =1;
        static private Dictionary<string, int> variables = new Dictionary<string, int>();
        static Compiler() {
            output_program.AppendLine("; ModuleID = 'main.c'");
            output_program.AppendLine("source_filename = \"main.c\"");
            output_program.AppendLine("target datalayout =\"e-m:w-p270:32:32-p271:32:32-p272:64:64-i64:64-i128:128-f80:128-n8:16:32:64-S128\"");
            output_program.AppendLine("target triple = \"x86_64-pc-windows-msvc19.38.33134\"");
            output_program.AppendLine("");
            output_program.AppendLine("$sprintf = comdat any");
            output_program.AppendLine("");
            output_program.AppendLine("$vsprintf = comdat any");
            output_program.AppendLine("");
            output_program.AppendLine("$_snprintf = comdat any");
            output_program.AppendLine("");
            output_program.AppendLine("$_vsnprintf = comdat any");
            output_program.AppendLine("");
            output_program.AppendLine("$_vsprintf_l = comdat any");
            output_program.AppendLine("");
            output_program.AppendLine("$_vsnprintf_l = comdat any");
            output_program.AppendLine("");
            output_program.AppendLine("$__local_stdio_printf_options = comdat any");
            output_program.AppendLine("");
            output_program.AppendLine("@__local_stdio_printf_options._OptionsStorage = internal global i64 0, align 8");
            output_program.AppendLine("declare dso_local i32 @putchar(i32 noundef) #1");
            output_program.AppendLine("define i32 @main()  {");
        }
        static public void CompileFuncCall(Expr program) {
            int left, right;
            int body;
            int condition;
            int leftResult;
            int rightResult;
            int res = 0;
            switch (program.function_name) {
                case "add":
                    CompileExpr(program.expressions[0]);
                    leftResult = instruction_p - 1; 

                    CompileExpr(program.expressions[1]);
                    rightResult = instruction_p - 1; 

                    output_program.AppendLine($"\n;  ---- ADD ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{leftResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{rightResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = add nsw i32 %a{instruction_p - 2}, %a{instruction_p - 3}");

                    output_program.AppendLine($"    %a{instruction_p++} = alloca i32, align 4");
                    output_program.AppendLine($"    store i32 %a{instruction_p - 2}, ptr %a{instruction_p - 1}, align 4");
                    break;
                case "mul":
                    CompileExpr(program.expressions[0]);
                     leftResult=instruction_p - 1;

                    CompileExpr(program.expressions[1]);
                    rightResult = instruction_p - 1;

                    output_program.AppendLine($"\n;  ---- MULL ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{leftResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{rightResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = mul nsw i32 %a{instruction_p - 2}, %a{instruction_p - 3}");

                    output_program.AppendLine($"    %a{instruction_p++} = alloca i32, align 4");
                    output_program.AppendLine($"    store i32 %a{instruction_p - 2}, ptr %a{instruction_p - 1}, align 4");
                    break;
                case "sub":
                    CompileExpr(program.expressions[0]);
                    leftResult = instruction_p - 1;

                    CompileExpr(program.expressions[1]);
                    rightResult = instruction_p - 1;

                    output_program.AppendLine($"\n;  ---- SUB ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{leftResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{rightResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = sub nsw i32 %a{instruction_p - 3}, %a{instruction_p - 2}");

                    output_program.AppendLine($"    %a{instruction_p++} = alloca i32, align 4");
                    output_program.AppendLine($"    store i32 %a{instruction_p - 2}, ptr %a{instruction_p - 1}, align 4");
                    break;
                //case "div":
                //    left = CompileExpr(program.expressions[0]);
                //    right = CompileExpr(program.expressions[1]);
                //    return left / right;
                //case "cmp":
                //    left = CompileExpr(program.expressions[0]);
                //    right = CompileExpr(program.expressions[1]);
                //    return left == right ? 1 : 0;
                //case "ls":
                //    left = CompileExpr(program.expressions[0]);
                //    right = CompileExpr(program.expressions[1]);
                //    return left <= right ? 1 : 0;
                //case "gr":
                //    left = CompileExpr(program.expressions[0]);
                //    right = CompileExpr(program.expressions[1]);
                //    return left >= right ? 1 : 0;
                //case "if":
                //    condition = CompileExpr(program.expressions[0]);
                //    if (condition == 1) return CompileExpr(program.expressions[1]);
                //    else return CompileExpr(program.expressions[2]);
                //case "mod":
                //    left = CompileExpr(program.expressions[0]);
                //    right = CompileExpr(program.expressions[1]);
                //    return left % right;
                //case "pass":
                //    return 0;
                //case "var":
                //    variables[program.expressions[0].variable_name] = 0;
                //    body = CompileExpr(program.expressions[0]);
                //    return body;
                //case "assign":
                //    variables[program.expressions[0].variable_name] = CompileExpr(program.expressions[1]);
                //    return CompileExpr(program.expressions[0]);
                //case "loop":
                //    condition = CompileExpr(program.expressions[0]);
                //    while (condition == 1) {
                //        res += CompileExpr(program.expressions[1]);
                //        condition = CompileExpr(program.expressions[0]);
                //    }
                //    return res;
                case "print":
                    CompileExpr(program.expressions[0]);
                    int printResult = instruction_p - 1;

                    output_program.AppendLine($"\n;  ---- PRINT ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{printResult}, align 4");
                    output_program.AppendLine($"    call i32 @putchar(i32 noundef %a{instruction_p - 1})");
                    break;
            };
        }
        static public void CompileExpr(Expr program) {
            switch (program.type) {
                case ExprType.Block:
                    int res = 0;
                    foreach (Expr block_expr in program.expressions)
                        CompileExpr(block_expr);
                    break;
                case ExprType.FunctionCall:
                       CompileFuncCall(program);
                    break;
                case ExprType.Number:
                    output_program.AppendLine($"\n;  ---- NUMBER ----");
                    output_program.AppendLine($"    %a{instruction_p} = alloca i32, align 4");
                    output_program.AppendLine($"    store i32 {program.value},ptr %a{instruction_p}, align 4");
                    instruction_p++;
                    //program.value;
                    break;
                case ExprType.Variable:
                    int out_value_var;
                    if (variables.TryGetValue(program.variable_name, out out_value_var)) {
                        //return out_value_var;
                    }
                    else throw new Exception($"Using of not identified variable: \'{program.variable_name}\'");
                    break;
            };
        }
    }
    class Program
    {
        public static void Main() {
            //string text = File.ReadAllText("./Program.txt");
            //string text = File.ReadAllText("./block_test.txt");
            //string text = File.ReadAllText("./loop_test.txt");
            string text = File.ReadAllText("./llvm_test.txt");
            Console.WriteLine(text);
            Parser.tokens = Lexer.Tokenize(text);
            //Console.WriteLine("result:\n");
            Expr[] program = Parser.ParseTokenArr();
            foreach (Expr ex in program) {
                Compiler.CompileExpr(ex);
            }
            Compiler.output_program.AppendLine("ret i32 0");
            Compiler.output_program.AppendLine("}");
            Console.WriteLine(Compiler.output_program.ToString());
            File.WriteAllText(".\\llvm_output.ll", Compiler.output_program.ToString());
        }
    }
}
