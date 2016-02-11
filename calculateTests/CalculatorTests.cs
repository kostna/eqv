using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

namespace CalculatorApp.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void OneActionTest()
        {
            var Calc = new Calculator(0);
            var Action = new Mock<ICalculatorAction>();

            Action.SetupGet(t => t.Unary).Returns(false);            
            Action.Setup(t => t.Calculate(It.IsAny<double>(), It.IsAny<double?>())).Returns(200);

            Calc.InputAction(Action.Object);
            Calc.InputNumber(50);

            Assert.AreEqual(200, Calc.CurrentValue);
            Action.Verify(a => a.Unary, Times.AtLeastOnce());
            Action.Verify(a => a.Calculate(It.Is<double>(d => d == 0), It.Is<double?>(d => d.HasValue && d.Value == 50)), Times.Once());
        }

        [TestMethod()]
        public void UnaryActionTest()
        {
            var Calc = new Calculator(0);
            var Action = new Mock<ICalculatorAction>();
                        
            Action.SetupGet(t => t.Unary).Returns(true);
            Action.Setup(t => t.Calculate(It.IsAny<double>(), It.IsAny<double?>())).Returns(200);

            Calc.InputAction(Action.Object);

            Assert.AreEqual(200, Calc.CurrentValue);
            Assert.AreEqual(WaitedInput.Action, Calc.InputWaited);

            Action.Verify(a => a.Unary, Times.AtLeastOnce());
            Action.Verify(a => a.Calculate(It.Is<double>(d => d == 0), It.Is<double?>(d => !d.HasValue)), Times.Once());
        }

    }
}