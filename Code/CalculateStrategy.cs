using System;
using System.Collections.Generic;

namespace CalculatorApp
{
    public class CalculateStrategy : ICalculateStrategy
    {
        public IDictionary<string, ICalculatorAction> Actions { get; private set; }
        public ICalculator Calculator { get; private set; }
        public IDataInput DataInput { get; private set; }

        public CalculateStrategy(IDictionary<string, ICalculatorAction> Actions, ICalculator Calculator, IDataInput DataInput)
        {
            this.Actions = Actions;
            this.Calculator = Calculator;
            this.DataInput = DataInput;
        }

        public bool StartCalculating()
        {
            bool Cancel;
            string Text = DataInput.Input("Введите начальное значение: ", out Cancel);
            if (Cancel)
                return false;

            Calculator.Reset(double.Parse(Text));
            return true;
        }

        public bool ReadAndExecuteNextAction()
        {
            if (Calculator.InputWaited != WaitedInput.Action)
                throw new Exception("нарушена логика работы калькулятора");

            bool Cancel;
            string ActionName = DataInput.Input("Укажите действие: ", out Cancel);
            if (Cancel)
                return false;

            ICalculatorAction Action;
            if (!Actions.TryGetValue(ActionName, out Action))
                throw new Exception("действие не найдено");

            Calculator.InputAction(Action);

            if (Calculator.InputWaited == WaitedInput.Numeric)
            {
                string Text = DataInput.Input("Введите следующее значение: ", out Cancel);
                if (Cancel)
                    return false;

                Calculator.InputNumber(double.Parse(Text));
            }

            return true;
        }
    }
}