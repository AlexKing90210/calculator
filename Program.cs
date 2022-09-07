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

            /*if (!regex.IsMatch(exp))
            {
                Console.WriteLine("Введеное выражение не соответствует ожидаемому");
            }*/

            if(this.handler.HandleInput(exp, regex))
            {
                string[] operands = regex.Split(exp);

                operands = operands.Where(x => x != "").ToArray();

                //Проверка первого операнда на число
                if(this.handler.isDigit(operands[0]))
                {
                    this.lOperand = operands[0];
                }
                else
                {
                    string message = $"Операнд {operands[0]} не является числом";
                    this.handler.ShowMessage(message);
                }

                //Проверка второго операнда на число
                if(this.handler.isDigit(operands[2]))
                {
                    this.rOperand = operands[2];
                }
                else
                {
                    string message = $"Операнд {operands[2]} не является числом";
                    this.handler.ShowMessage(message);
                }

                //Проверка на то, что оператор находится в списке допустимых
                if(this.handler.isValidChar(operands[1].ToCharArray()[0]))
                {
                    this.arithmeticSign = operands[1].ToCharArray()[0];
                }
                else
                {
                    string message = $"Оператор {operands[1]} не является допустимым для данной программы";
                    this.handler.ShowMessage(message);
                }

                

            }
            else
            {
                string message = $"Введеное выражение не соответствует ожидаемому";
                this.handler.ShowMessage(message);
            }

        }

    }


    class ErrorHandler
    {

        public bool HandleInput(string exp, Regex regex)
        {

            //Проверка наличия операнда
            if (!regex.IsMatch(exp))
            {
                /*Console.WriteLine("Введеное выражение не соответствует ожидаемому");
                string message = $"Введеное выражение не соответствует ожидаемому";
                this.ShowMessage(message);*/
                return false;
            }
            return true;

        }

        public bool isNotNull(string value, char operation)
        {
            if((operation == '/') & (Convert.ToSingle(value) == 0))
            {
                /*Console.WriteLine("На ноль делить нельзя!");
                string message = $"На ноль делить нельзя!";
                this.ShowMessage(message);*/
                return false;
            }
            return true;
        }

        public bool isValidChar(char value)
        {
            char[] validSymbols = new char[] { '/', '*', '-', '+' };
            if(!Array.Exists(validSymbols, element => element == value))
            {
                /*Console.WriteLine("Оператор {0} не является допустимым для данной программы", value);
                string message = $"Оператор {value} не является допустимым для данной программы";
                this.ShowMessage(message);*/
                return false;
            }
            return true;
        }

        public bool isDigit(string value)
        {
            double number;

            if(!double.TryParse(value, out number))
            {
                /*Console.WriteLine("Операнд {0} не является числом", value);
                string message = $"Операнд {value} не является числом";
                this.ShowMessage(message);*/
                return false;
            }
            return true;
            

        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

    }


    class Calculate
    {
        private ErrorHandler handler;

        public ErrorHandler Handler
        {
            get { return handler; }
            set { handler = value; }
        }

        public void CalculateResult(Parser parser)
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
                //Проверка для исключения деления на ноль
                if (!this.handler.isNotNull(parser.ROperand, parser.ArithmeticSign))
                {
                    string message = $"На ноль делить нельзя!";
                    this.handler.ShowMessage(message);
                }
                else
                {
                    result = Convert.ToSingle(parser.LOperand) / Convert.ToSingle(parser.ROperand);
                    Console.WriteLine("Результат: {0}", result);
                }
                
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
            while(true)
            {
                string expressions = "";

                Console.WriteLine("Введите выражение:");
                expressions = Console.ReadLine();

                ErrorHandler errorHandler = new ErrorHandler();

                Parser parser = new Parser();
                parser.Handler = errorHandler;
                parser.Parse(expressions);

                Calculate res = new Calculate();
                res.Handler = errorHandler;
                res.CalculateResult(parser);

                Console.WriteLine();

                if(Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    break;
                }
               
            }
        }

    }

}


