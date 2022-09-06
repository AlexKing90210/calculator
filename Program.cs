using System;
using System.Text.RegularExpressions;


namespace Calculator
{
    class Parser
    {
        private string lOperand;
        private string rOperand;
        private char arithmeticSign;

        public string LOperand
        {
            get { return lOperand; }
            set { lOperand = value; }
        }

        public string ROperand
        {
            get { return rOperand; }
            set { rOperand = value; }
        }

        public char ArithmeticSign
        {
            get { return arithmeticSign; }
            set { arithmeticSign = value; }
        }

        public void Parse(string expression)
        {
            string exp = expression.Replace(" ", "");
            //Регулярное выражение для проверки введенного значения
            Regex regex = new Regex(@"^-*\d*[\/\*+-]{1}-*\d*$");

            if (!regex.IsMatch(exp))
            {
                Console.WriteLine("Введеное выражение не соответствует ожидаемому");
            }

            //Парсинг операндов
            string pattern = @"[/*+-]+";
            Regex rgx = new Regex(pattern);

            string[] operands = rgx.Split(exp);

            //Парсинг оператора
            string patternOp = @"\d"; 
            Regex rgxOp = new Regex(patternOp);

            string[] operators = rgxOp.Split(exp);
            /*foreach (string op in operators)
            {
                Console.WriteLine(op);
            }
            Console.WriteLine(operators.Length);*/

            this.lOperand = operands[0];
            this.rOperand = operands[1];
            this.arithmeticSign = operators[1].ToCharArray()[0];

        }

    }

    class Calculate
    {
        public static void CalculateResult(Parser parser)
        {
            float result = 0;
            if(parser.ArithmeticSign == '+')
            {
                result = Convert.ToSingle(parser.LOperand) + Convert.ToSingle(parser.ROperand);
                Console.WriteLine(result);
            }
            if (parser.ArithmeticSign == '*')
            {
                result = Convert.ToSingle(parser.LOperand) * Convert.ToSingle(parser.ROperand);
                Console.WriteLine(result);
            }
            if (parser.ArithmeticSign == '/')
            {
                result = Convert.ToSingle(parser.LOperand) / Convert.ToSingle(parser.ROperand);
                Console.WriteLine(result);
            }
            if (parser.ArithmeticSign == '-')
            {
                result = Convert.ToSingle(parser.LOperand) - Convert.ToSingle(parser.ROperand);
                Console.WriteLine(result);
            }


        }

    }

    class ErrorHandler
    {
        public string ErrorMessage;

        public void ShowMessage(string message)
        {
            ErrorMessage = message;
            Console.WriteLine(ErrorMessage);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            string expressions = "";

            Console.WriteLine("Введите выражение:");
            expressions = Console.ReadLine();
            
            Parser parser = new Parser();
            parser.Parse(expressions);

            Calculate res = new Calculate();
            Calculate.CalculateResult(parser);

        }

    }

}

