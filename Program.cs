using System;
using System.Text.RegularExpressions;


namespace Calculator
{
    class Parser
    {
        private string lOperand;
        private string rOperand;
        private char arithmeticSign;
        private ErrorHandler handler;

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

        public ErrorHandler Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        public void Parse(string expression)
        {
            string exp = expression.Replace(" ", "");
            //Регулярное выражение для проверки введенного значения
            Regex regex = new Regex(@"^(-*\d+)([/\*\+\-])(-*\d+)$");

            if (!regex.IsMatch(exp))
            {
                Console.WriteLine("Введеное выражение не соответствует ожидаемому");
            }

            string[] operands = regex.Split(exp);

            operands = operands.Where(x => x != "").ToArray();

            this.lOperand = operands[0];
            this.rOperand = operands[2];
            this.arithmeticSign = operands[1].ToCharArray()[0];

        }

    }


    class ErrorHandler
    {

        public void Handler(string expression)
        {

            //Проверка наличия операнда
            string exp = expression.Replace(" ", "");
            //Регулярное выражение для проверки введенного значения
            Regex regex = new Regex(@"^(-*\d+)([/\*\+\-])(-*\d+)$");
            if (!regex.IsMatch(exp))
            {
                Console.WriteLine("Введеное выражение не соответствует ожидаемому");
                string message = $"Введеное выражение не соответствует ожидаемому";
                this.ShowMessage(message);
            }

        }

        public void isNotNull(string value, char operation)
        {
            if((operation == '/') & (Convert.ToSingle(value) == 0))
            {
                Console.WriteLine("На ноль делить нельзя!");
                string message = $"На ноль делить нельзя!";
                this.ShowMessage(message);
            }
        }

        public void isValidChar(char value)
        {
            char[] validSymbols = new char[] { '/', '*', '-', '+' };
            if(Array.Exists(validSymbols, element => element == value))
            {
                Console.WriteLine("Оператор {0} не является допустимым для данной программы", value);
                string message = $"Оператор {value} не является допустимым для данной программы";
                this.ShowMessage(message);
            }
        }

        public void isDigit(string value)
        {
            double number;

            if(!double.TryParse(value, out number))
            {
                Console.WriteLine("Операнд {0} не является числом", value);
                string message = $"Операнд {value} не является числом";
                this.ShowMessage(message);
            }

        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

    }


    class Calculate
    {
        public static void CalculateResult(Parser parser)
        {
            float result = 0;
            if (parser.ArithmeticSign == '+')
            {
                result = Convert.ToSingle(parser.LOperand) + Convert.ToSingle(parser.ROperand);
                Console.WriteLine("Результат: {0}", result);
            }
            if (parser.ArithmeticSign == '*')
            {
                result = Convert.ToSingle(parser.LOperand) * Convert.ToSingle(parser.ROperand);
                Console.WriteLine("Результат: {0}", result);
            }
            if (parser.ArithmeticSign == '/')
            {
                result = Convert.ToSingle(parser.LOperand) / Convert.ToSingle(parser.ROperand);
                Console.WriteLine("Результат: {0}", result);
            }
            if (parser.ArithmeticSign == '-')
            {
                result = Convert.ToSingle(parser.LOperand) - Convert.ToSingle(parser.ROperand);
                Console.WriteLine("Результат: {0}", result);
            }

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


