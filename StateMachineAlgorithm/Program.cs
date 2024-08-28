//用户输入任一字符，判断这个字符是否是合法数字，允许正负号，小数点，科学计数法
//合法数字的定义如下：
//1. 数字可以有前导空格，后导空格
//2. 数字可以有正负号
//3. 数字可以是小数，小数点前后至少有一位数字
//4. 数字可以是科学计数法，指数部分必须是整数，格式为e或E后面跟着整数
//5. 数字可以是整数
//6. 数字不能是其他字符
//7. 数字不能以小数点结尾
//8. 数字不能以e或E结尾
//9. 数字不能以正负号结尾
//10. 数字不能以空格结尾
//11. 数字不能以空格开头

//例如，以下字符串都是合法数字：
//"0" => true
//" 0.1 " => true
//"abc" => false
//"1 a" => false
//"2e10" => true
//" -90e3   " => true
//" 1e" => false
//"e3" => false
//" 6e-1" => true
//" 99e2.5 " => false


//状态机算法的基本步骤如下：
//1. 定义状态：首先，我们需要定义状态，状态通常是一个枚举类型。
//2. 定义状态转移表：然后，我们需要定义状态转移表，即根据当前状态和输入字符，决定下一个状态。
//3. 状态转移：接着，我们需要根据状态转移表，不断地改变状态。
//4. 判断结果：最后，我们根据最终的状态，判断是否符合要求。

//状态机算法的关键是定义状态和状态转移表，这是整个算法的核心。

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input a number:");
            while (true)
            {
                string? input = Console.ReadLine();
                Console.WriteLine(IsNumber(input));
            }
        }

        // 定义状态
        public enum State
        {
            /// <summary>
            /// 开始
            /// </summary>
            Start,
            /// <summary>
            /// 符号
            /// </summary>
            Sign,
            /// <summary>
            /// 数字
            /// </summary>
            Integer,
            /// <summary>
            /// 小数点
            /// </summary>
            Point,
            /// <summary>
            /// 小数
            /// </summary>
            Decimal,
            /// <summary>
            /// 指数符号
            /// </summary>
            E,
            /// <summary>
            /// 指数
            /// </summary>
            Exponent,
            /// <summary>
            /// 结束
            /// </summary>
            End
        }

        public static bool IsNumber(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            // 定义状态转移表
            Dictionary<State, Dictionary<char, State>> transfer = new Dictionary<State, Dictionary<char, State>>();
            transfer[State.Start] = new Dictionary<char, State>
            {
                [' '] = State.Start,
                ['+'] = State.Sign,
                ['-'] = State.Sign,
                ['0'] = State.Integer,
                ['1'] = State.Integer,
                ['2'] = State.Integer,
                ['3'] = State.Integer,
                ['4'] = State.Integer,
                ['5'] = State.Integer,
                ['6'] = State.Integer,
                ['7'] = State.Integer,
                ['8'] = State.Integer,
                ['9'] = State.Integer,
                ['.'] = State.Point,
                ['e'] = State.E,
                ['E'] = State.E
            };
            transfer[State.Sign] = new Dictionary<char, State>
            {
                ['0'] = State.Integer,
                ['1'] = State.Integer,
                ['2'] = State.Integer,
                ['3'] = State.Integer,
                ['4'] = State.Integer,
                ['5'] = State.Integer,
                ['6'] = State.Integer,
                ['7'] = State.Integer,
                ['8'] = State.Integer,
                ['9'] = State.Integer,
                ['.'] = State.Point
            };
            transfer[State.Integer] = new Dictionary<char, State>
            {
                [' '] = State.End,
                ['0'] = State.Integer,
                ['1'] = State.Integer,
                ['2'] = State.Integer,
                ['3'] = State.Integer,
                ['4'] = State.Integer,
                ['5'] = State.Integer,
                ['6'] = State.Integer,
                ['7'] = State.Integer,
                ['8'] = State.Integer,
                ['9'] = State.Integer,
                ['.'] = State.Decimal,
                ['e'] = State.E,
                ['E'] = State.E
            };
            transfer[State.Point] = new Dictionary<char, State>
    {
                ['0'] = State.Decimal,
                ['1'] = State.Decimal,
                ['2'] = State.Decimal,
                ['3'] = State.Decimal,
                ['4'] = State.Decimal,
                ['5'] = State.Decimal,
                ['6'] = State.Decimal,
                ['7'] = State.Decimal,
                ['8'] = State.Decimal,
                ['9'] = State.Decimal
            };
            transfer[State.Decimal] = new Dictionary<char, State>
            {
                [' '] = State.End,
                ['0'] = State.Decimal,
                ['1'] = State.Decimal,
                ['2'] = State.Decimal,
                ['3'] = State.Decimal,
                ['4'] = State.Decimal,
                ['5'] = State.Decimal,
                ['6'] = State.Decimal,
                ['7'] = State.Decimal,
                ['8'] = State.Decimal,
                ['9'] = State.Decimal,
                ['e'] = State.E,
                ['E'] = State.E
            };
            transfer[State.E] = new Dictionary<char, State>
            {
                ['+'] = State.Exponent,
                ['-'] = State.Exponent,
                ['0'] = State.Exponent,
                ['1'] = State.Exponent,
                ['2'] = State.Exponent,
                ['3'] = State.Exponent,
                ['4'] = State.Exponent,
                ['5'] = State.Exponent,
                ['6'] = State.Exponent,
                ['7'] = State.Exponent,
                ['8'] = State.Exponent,
                ['9'] = State.Exponent
            };
            transfer[State.Exponent] = new Dictionary<char, State>
            {
                [' '] = State.End,
                ['0'] = State.Exponent,
                ['1'] = State.Exponent,
                ['2'] = State.Exponent,
                ['3'] = State.Exponent,
                ['4'] = State.Exponent,
                ['5'] = State.Exponent,
                ['6'] = State.Exponent,
                ['7'] = State.Exponent,
                ['8'] = State.Exponent,
                ['9'] = State.Exponent
            };
            transfer[State.End] = new Dictionary<char, State>
            {
                [' '] = State.End
            };

            // 状态转移
            State state = State.Start;
            foreach (char c in s)
            {
                if (!transfer[state].ContainsKey(c))
                {
                    return false;
                }
                state = transfer[state][c];
            }

            // 判断结果
            return state == State.Integer || state == State.Decimal || state == State.Exponent || state == State.End;
        }
    }
}
//这段代码中，我们首先定义了一个枚举类型 State，表示状态。然后，我们定义了一个状态转移表 transfer，表示状态之间的转移关系。接着，我们根据输入的字符串 s，不断地改变状态，最终判断是否符合要求。
