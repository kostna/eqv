using System;

namespace CalculatorApp
{
    public class Calculator : ICalculator
    {
        public double CurrentValue { get; private set; }
        public WaitedInput InputWaited { get; private set; }

        private double? _Right;
        private ICalculatorAction Action;

        private void ExecuteAction()
        {
            if (Action == null)
                throw new Exception("action not set");

            CurrentValue = Action.Calculate(CurrentValue, _Right);

            _Right = null;
            Action = null;
            InputWaited = WaitedInput.Action;
        }

        public Calculator(double InitalValue)
        {
            Reset(InitalValue);
        }

        public void Reset(double Number)
        {
            CurrentValue = Number;
            InputWaited = WaitedInput.Action;

            _Right = null;
            Action = null;
        }

        public void InputNumber(double Number)
        {
            if (InputWaited != WaitedInput.Numeric)
                throw new Exception("numeric input requered");

            _Right = Number;
            ExecuteAction();
        }

        public void InputAction(ICalculatorAction Action)
        {
            if (InputWaited != WaitedInput.Action)
                throw new Exception("action input requered");

            if (Action == null)
                throw new ArgumentNullException("Action");

            this.Action = Action;
            if (Action.Unary)
                ExecuteAction();
            else
                InputWaited = WaitedInput.Numeric;
        }
    }
}