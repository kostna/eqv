namespace CalculatorApp
{
    public interface ICalculatorAction
    {
        bool Unary { get; }
        double Calculate(double Left, double? Right);
    }

    public enum WaitedInput { Numeric, Action }

    public interface ICalculator
    {
        void InputNumber(double Number);
        void InputAction(ICalculatorAction Action);

        void Reset(double Number);

        WaitedInput InputWaited { get; }

        double CurrentValue { get; }
    }

    public interface IDataInput
    {
        string Input(string Promt, out bool Cancel);
    }

    public interface ICalculateStrategy
    {
        bool StartCalculating();
        bool ReadAndExecuteNextAction();
    }

}