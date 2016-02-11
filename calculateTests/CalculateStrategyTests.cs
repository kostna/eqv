using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using Moq;

namespace CalculatorApp.Tests
{
    [TestClass()]
    public class CalculateStrategyTests
    {
        [TestMethod()]
        public void StartCalculatingTest()
        {
            var DataInput = new Mock<IDataInput>();

            bool _cancel = false;
            DataInput.Setup(m => m.Input(It.IsAny<string>(), out _cancel)).Returns("123");

            var Calc = new Mock<ICalculator>();            
            Calc.Setup(m => m.Reset(It.IsAny<double>()));
            var Strategy = new CalculateStrategy(new Dictionary<string, ICalculatorAction>(), Calc.Object, DataInput.Object);

            bool Started = Strategy.StartCalculating();
            Assert.AreEqual(true, Started, "вычисление должно было начаться ");
            Calc.Verify(c => c.Reset(It.Is<double>(t => t == 123)), Times.Once());

            _cancel = true;
            DataInput.Setup(m => m.Input(It.IsAny<string>(), out _cancel)).Returns("123");

            Started = Strategy.StartCalculating();
            Assert.AreEqual(false, Started, "вычисление не должно было начаться ");
            Calc.Verify(c => c.Reset(It.IsAny<double>()), Times.Once());
        }        
    }
}