using System;

namespace CalculatorApp
{

    public abstract class BinaryCalculatorAction : ICalculatorAction
    {
        protected abstract double DoCalculate(double Left, double Right);

        public bool Unary { get { return false; } }
        public double Calculate(double Left, double? Right)
        {
            if (!Right.HasValue)
                throw new Exception("Второе значение не указано");

            return DoCalculate(Left, Right.Value);
        }
    }

    public class AdditionCalculatorAction : BinaryCalculatorAction
    {
        protected override double DoCalculate(double Left, double Right)
        {
            return Left + Right;
        }
    }

    public class SubtractionCalculatorAction : BinaryCalculatorAction
    {
        protected override double DoCalculate(double Left, double Right)
        {
            return Left - Right;
        }
    }

    public class MultiplicationCalculatorAction : BinaryCalculatorAction
    {
        protected override double DoCalculate(double Left, double Right)
        {
            return Left * Right;
        }
    }

    public class DivisionCalculatorAction : BinaryCalculatorAction
    {
        protected override double DoCalculate(double Left, double Right)
        {
            if (Right == 0)
                throw new Exception("делитель не может быть равен 0");

            return Left / Right;
        }
    }
}