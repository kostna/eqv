using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    public struct DataInput : IDataInput
    {
        public string Input(string Promt, out bool Cancel)
        {
            if (Promt != null)
                Console.Write(Promt);

            string Temp = Console.ReadLine();
            Cancel = string.IsNullOrWhiteSpace(Temp);
            return Temp;
        }
    }

    class Program
    {
        static bool DoCalculate(ICalculateStrategy Strategy, ICalculator Calc)
        {
            if (!Strategy.StartCalculating())
                return false;

            while (Strategy.ReadAndExecuteNextAction())
            {
                Console.WriteLine("\nТекущее значение: {0}\n", Calc.CurrentValue);
            }

            return true;
        }

        static void Main(string[] args)
        {
            try
            {
                var CalcActions = new Dictionary<string, ICalculatorAction>()
                {
                    {"+", new AdditionCalculatorAction() },
                    {"-", new SubtractionCalculatorAction() },
                    {"*", new MultiplicationCalculatorAction() },
                    {"/", new DivisionCalculatorAction() }
                };

                var Calc = new Calculator(0);

                var Strategy = new CalculateStrategy(CalcActions, Calc, new DataInput());
                if (DoCalculate(Strategy, Calc))
                    Console.WriteLine("\nВычисления завершены. Окончательное значение: {0}\n", Calc.CurrentValue);
                else
                    Console.WriteLine("Вычисления отменены");
            }
            catch (Exception E)
            {
                Console.WriteLine("Во время работы программы произошла ошибка: {0}", E.Message);
            }

            Console.Write("Нажмите ENTER для завершения работы...");
            Console.ReadLine();
        }
    }
}