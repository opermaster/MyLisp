using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq.Expressions;
using System.Net;
using System.Drawing;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
            output_program.AppendLine("");
            output_program.AppendLine("@__local_stdio_printf_options._OptionsStorage = internal global i64 0, align 8");
            output_program.AppendLine("declare dso_local i32 @putchar(i32 noundef) #1");
            output_program.AppendLine("define dso_local i32 @Power(i32 noundef %0, i32 noundef %1) #0 {");
            output_program.AppendLine("  %3 = alloca i32, align 4");
            output_program.AppendLine("  %4 = alloca i32, align 4");
            output_program.AppendLine("  %5 = alloca i32, align 4");
            output_program.AppendLine("  %6 = alloca i32, align 4");
            output_program.AppendLine("  store i32 %1, ptr %3, align 4");
            output_program.AppendLine("  store i32 %0, ptr %4, align 4");
            output_program.AppendLine("  store i32 1, ptr %5, align 4");
            output_program.AppendLine("  store i32 0, ptr %6, align 4");
            output_program.AppendLine("  br label %7");
            output_program.AppendLine("");
            output_program.AppendLine("7:                                                ; preds = %15, %2");
            output_program.AppendLine("  %8 = load i32, ptr %6, align 4");
            output_program.AppendLine("  %9 = load i32, ptr %3, align 4");
            output_program.AppendLine("  %10 = icmp slt i32 %8, %9");
            output_program.AppendLine("  br i1 %10, label %11, label %18");
            output_program.AppendLine("");
            output_program.AppendLine("11:                                               ; preds = %7");
            output_program.AppendLine("  %12 = load i32, ptr %4, align 4");
            output_program.AppendLine("  %13 = load i32, ptr %5, align 4");
            output_program.AppendLine("  %14 = mul nsw i32 %13, %12");
            output_program.AppendLine("  store i32 %14, ptr %5, align 4");
            output_program.AppendLine("  br label %15");
            output_program.AppendLine("");
            output_program.AppendLine("15:                                               ; preds = % 11");
            output_program.AppendLine("  %16 = load i32, ptr %6, align 4");
            output_program.AppendLine("  %17 = add nsw i32 %16, 1");
            output_program.AppendLine("  store i32 %17, ptr %6, align 4");
            output_program.AppendLine("  br label %7, !llvm.loop!5");
            output_program.AppendLine("");
            output_program.AppendLine("18:                                               ; preds = % 7");
            output_program.AppendLine("  %19 = load i32, ptr %5, align 4");
            output_program.AppendLine("  ret i32 %19");
            output_program.AppendLine("}");
            output_program.AppendLine("");
            output_program.AppendLine("");
            output_program.AppendLine("define dso_local void @Print(i32 noundef %0) #0 {");
            output_program.AppendLine("  %2 = alloca i32, align 4");
            output_program.AppendLine("  %3 = alloca i32, align 4");
            output_program.AppendLine("  %4 = alloca i32, align 4");
            output_program.AppendLine("  %5 = alloca i32, align 4");
            output_program.AppendLine("  store i32 %0, ptr %2, align 4");
            output_program.AppendLine("  %6 = load i32, ptr %2, align 4");
            output_program.AppendLine("  %7 = icmp slt i32 %6, 0");
            output_program.AppendLine("  br i1 %7, label %8, label %11");
            output_program.AppendLine("");
            output_program.AppendLine("8:                                                ; preds = %1");
            output_program.AppendLine("  %9 = load i32, ptr %2, align 4");
            output_program.AppendLine("  %10 = sub nsw i32 0, %9");
            output_program.AppendLine("  store i32 %10, ptr %2, align 4");
            output_program.AppendLine("  br label %11");
            output_program.AppendLine("");
            output_program.AppendLine("11:                                               ; preds = %8, %1");
            output_program.AppendLine("  %12 = load i32, ptr %2, align 4");
            output_program.AppendLine("  store i32 %12, ptr %3, align 4");
            output_program.AppendLine("  store i32 0, ptr %4, align 4");
            output_program.AppendLine("  br label %13");
            output_program.AppendLine("");
            output_program.AppendLine("13:                                               ; preds = %16, %11");
            output_program.AppendLine("  %14 = load i32, ptr %3, align 4");
            output_program.AppendLine("  %15 = icmp sgt i32 %14, 0");
            output_program.AppendLine("  br i1 %15, label %16, label %21");
            output_program.AppendLine("");
            output_program.AppendLine("16:                                               ; preds = %13");
            output_program.AppendLine("  %17 = load i32, ptr %3, align 4");
            output_program.AppendLine("  %18 = sdiv i32 %17, 10");
            output_program.AppendLine("  store i32 %18, ptr %3, align 4");
            output_program.AppendLine("  %19 = load i32, ptr %4, align 4");
            output_program.AppendLine("  %20 = add nsw i32 %19, 1");
            output_program.AppendLine("  store i32 %20, ptr %4, align 4");
            output_program.AppendLine("  br label %13, !llvm.loop !7");
            output_program.AppendLine("");
            output_program.AppendLine("21:                                               ; preds = %13");
            output_program.AppendLine("  %22 = load i32, ptr %4, align 4");
            output_program.AppendLine("  %23 = sub nsw i32 %22, 1");
            output_program.AppendLine("  %24 = call i32 @Power(i32 noundef 10, i32 noundef %23)");
            output_program.AppendLine("  store i32 %24, ptr %5, align 4");
            output_program.AppendLine("  br label %25");
            output_program.AppendLine("");
            output_program.AppendLine("25:                                               ; preds = %28, %21");
            output_program.AppendLine("  %26 = load i32, ptr %5, align 4");
            output_program.AppendLine("  %27 = icmp sgt i32 %26, 0");
            output_program.AppendLine("  br i1 %27, label %28, label %39");
            output_program.AppendLine("");
            output_program.AppendLine("28:                                               ; preds = %25");
            output_program.AppendLine("  %29 = load i32, ptr %2, align 4");
            output_program.AppendLine("  %30 = load i32, ptr %5, align 4");
            output_program.AppendLine("  %31 = sdiv i32 %29, %30");
            output_program.AppendLine("  %32 = add nsw i32 %31, 48");
            output_program.AppendLine("  %33 = call i32 @putchar(i32 noundef %32)");
            output_program.AppendLine("  %34 = load i32, ptr %5, align 4");
            output_program.AppendLine("  %35 = load i32, ptr %2, align 4");
            output_program.AppendLine("  %36 = srem i32 %35, %34");
            output_program.AppendLine("  store i32 %36, ptr %2, align 4");
            output_program.AppendLine("  %37 = load i32, ptr %5, align 4");
            output_program.AppendLine("  %38 = sdiv i32 %37, 10");
            output_program.AppendLine("  store i32 %38, ptr %5, align 4");
            output_program.AppendLine("  br label %25, !llvm.loop !8");
            output_program.AppendLine("");
            output_program.AppendLine("39:                                               ; preds = %25");
            output_program.AppendLine("  %40 = call i32 @putchar(i32 noundef 10)");
            output_program.AppendLine("  ret void");
            output_program.AppendLine("    }");
            output_program.AppendLine("");
            output_program.AppendLine("define i32 @main()  {");
            
        }
static public void CompileFuncCall(Expr program) {
            int left, right;
            int body;
            int condition;
            int leftResult;
            int rightResult;
            int true_false = 0;
            int loop_body_cond = 0;
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
                case "div":
                    CompileExpr(program.expressions[0]);
                    leftResult = instruction_p - 1;

                    CompileExpr(program.expressions[1]);
                    rightResult = instruction_p - 1;

                    output_program.AppendLine($"\n;  ---- DIV ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{leftResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{rightResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = udiv i32 %a{instruction_p - 3}, %a{instruction_p - 2}");

                    output_program.AppendLine($"    %a{instruction_p++} = alloca i32, align 4");
                    output_program.AppendLine($"    store i32 %a{instruction_p - 2}, ptr %a{instruction_p - 1}, align 4");
                    break;
                case "cmp":
                    CompileExpr(program.expressions[0]);
                    leftResult = instruction_p - 1;

                    CompileExpr(program.expressions[1]);
                    rightResult = instruction_p - 1;

                    output_program.AppendLine($"\n;  ---- CMP ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{leftResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{rightResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = icmp eq i32 %a{instruction_p - 3}, %a{instruction_p - 2}");

                    //output_program.AppendLine($"    %a{instruction_p++} = alloca i32, align 4");
                    //output_program.AppendLine($"    store i32 %a{instruction_p - 2}, ptr %a{instruction_p - 1}, align 4");
                    break;
                case "ls":
                    CompileExpr(program.expressions[0]);
                    leftResult = instruction_p - 1;

                    CompileExpr(program.expressions[1]);
                    rightResult = instruction_p - 1;

                    output_program.AppendLine($"\n;  ---- LS <= ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{leftResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{rightResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = icmp ule i32 %a{instruction_p - 3}, %a{instruction_p - 2}");
                    break;
                case "gr":
                    CompileExpr(program.expressions[0]);
                    leftResult = instruction_p - 1;

                    CompileExpr(program.expressions[1]);
                    rightResult = instruction_p - 1;

                    output_program.AppendLine($"\n;  ---- GR >= ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{leftResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{rightResult}, align 4");
                    output_program.AppendLine($"    %a{instruction_p++} = icmp uge i32 %a{instruction_p - 3}, %a{instruction_p - 2}");
                    break;
                case "if":
                    output_program.AppendLine($"\n;  ---- IF ----");
                    CompileExpr(program.expressions[0]);
                    output_program.AppendLine($"    br i1 %a{instruction_p - 1}, label %atrue{instruction_p + 1} , label %afalse{instruction_p + 1}");

                    true_false = instruction_p + 1;

                    output_program.AppendLine($"atrue{true_false}:");
                    CompileExpr(program.expressions[1]);
                    output_program.AppendLine($"   br label %aend{true_false}");

                    output_program.AppendLine($"afalse{true_false}:");
                    CompileExpr(program.expressions[2]);
                    output_program.AppendLine($"   br label %aend{true_false}");

                    output_program.AppendLine($"aend{true_false}:");
                    break;
                //case "mod":
                //    left = CompileExpr(program.expressions[0]);
                //    right = CompileExpr(program.expressions[1]);
                //    return left % right;
                //case "pass":
                //    return 0;
                case "var":
                    output_program.Insert(206,$"\n;  ---- VAR DECLARE ----\n");
                    output_program.Insert(234,$"@{program.expressions[0].variable_name} = dso_local global i32 0, align 4\n");
                    break;
                case "assign":
                    CompileExpr(program.expressions[1]);
                    output_program.AppendLine($"\n;  ---- VAR ASSIGN ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{instruction_p-2}, align 4");
                    output_program.AppendLine($"    store i32 %a{instruction_p - 1}, ptr @{program.expressions[0].variable_name}, align 4");
                    break;
                case "loop":
                    output_program.AppendLine($"\n;  ---- LOOP ----");
                    output_program.AppendLine($"    br label %loopcond{instruction_p++}");
                    loop_body_cond = instruction_p-1;
                    output_program.AppendLine($"loopcond{loop_body_cond}:");

                    CompileExpr(program.expressions[0]);
                    output_program.AppendLine($"    br i1 %a{instruction_p - 1}, label %loopbody{loop_body_cond}, label %loopend{loop_body_cond}");

                    output_program.AppendLine($"loopbody{loop_body_cond}:");
                    CompileExpr(program.expressions[1]);
                    
                    output_program.AppendLine($"    br label %loopcond{loop_body_cond}, !llvm.loop !9");
                    output_program.AppendLine($"loopend{loop_body_cond}:");
                    break;
                //condition = CompileExpr(program.expressions[0]);
                //while (condition == 1) {
                //    CompileExpr(program.expressions[1]);
                //    condition = CompileExpr(program.expressions[0]);
                //}
                case "print":
                    CompileExpr(program.expressions[0]);
                    int printResult = instruction_p - 1;
                    output_program.AppendLine($"\n;  ---- PRINT ----");
                    output_program.AppendLine($"    %a{instruction_p++} = load i32, ptr %a{printResult}, align 4");
                    output_program.AppendLine($"    call void @Print(i32 noundef %a{instruction_p - 1})");
                    break;
            };
        }
        static public void CompileExpr(Expr program) {
            switch (program.type) {
                case ExprType.Block:
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
                    break;
                case ExprType.Variable:
                    output_program.AppendLine($"\n;  ---- VARIABLE USAGE ----");
                    output_program.AppendLine($"    %a{instruction_p++} = alloca i32, align 4");
                    output_program.AppendLine($"    %a{instruction_p}{program.variable_name}_value = load i32, ptr @{program.variable_name}");
                    output_program.AppendLine($"    store i32 %a{instruction_p}{program.variable_name}_value, ptr %a{instruction_p-1}, align 4");
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
            Compiler.output_program.AppendLine("!llvm.module.flags = !{ !0, !1, !2, !3}");
            Compiler.output_program.AppendLine("!llvm.ident = !{ !4}");
            Compiler.output_program.AppendLine("");
            Compiler.output_program.AppendLine("!0 = !{ i32 1, !\"wchar_size\", i32 2}");
            Compiler.output_program.AppendLine("!1 = !{ i32 8, !\"PIC Level\", i32 2}");
            Compiler.output_program.AppendLine("!2 = !{ i32 7, !\"uwtable\", i32 2}");
            Compiler.output_program.AppendLine("!3 = !{ i32 1, !\"MaxTLSAlign\", i32 65536}");
            Compiler.output_program.AppendLine("!4 = !{ !\"clang version 18.1.8\"}");
            Compiler.output_program.AppendLine("!5 = distinct!{ !5, !6}");
            Compiler.output_program.AppendLine("!6 = !{ !\"llvm.loop.mustprogress\"}");
            Compiler.output_program.AppendLine("!7 = distinct!{ !7, !6}");
            Compiler.output_program.AppendLine("!8 = distinct!{ !8, !6}");
            Compiler.output_program.AppendLine("!9 = distinct!{ !9, !6}");
            Console.WriteLine(Compiler.output_program.ToString());
            File.WriteAllText(".\\llvm_output.ll", Compiler.output_program.ToString());
        }
    }
}
