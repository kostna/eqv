using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Tests
{
    [TestClass()]
    public class BinaryCalculatorActionTests
    {
        private void TestWithNull(ICalculatorAction Action)
        {
            try
            {
                Action.Calculate(10, null);
                Assert.Fail("должна была произойти ошибка, т.к. второй аргумент null");
            }
            catch (Exception) { }
        }

        [TestMethod()]
        public void AdditionTest()
        {
            var Action = new AdditionCalculatorAction();
            Assert.AreEqual(false, Action.Unary);

            Assert.AreEqual(10, Action.Calculate(3, 7));
            Assert.AreEqual(10, Action.Calculate(3.4, 6.6));

            TestWithNull(Action);
        }

        [TestMethod()]
        public void SubtractionTest()
        {
            var Action = new SubtractionCalculatorAction();
            Assert.AreEqual(false, Action.Unary);

            Assert.AreEqual(-4, Action.Calculate(3, 7));
            Assert.AreEqual(20, Action.Calculate(40, 20));

            TestWithNull(Action);
        }

        [TestMethod()]
        public void MultiplicationTest()
        {
            var Action = new MultiplicationCalculatorAction();
            Assert.AreEqual(false, Action.Unary);

            Assert.AreEqual(21, Action.Calculate(3, 7));
            Assert.AreEqual(800, Action.Calculate(40, 20));

            TestWithNull(Action);
        }

        [TestMethod()]
        public void DivisionTest()
        {
            var Action = new DivisionCalculatorAction();
            Assert.AreEqual(false, Action.Unary);

            Assert.AreEqual(2, Action.Calculate(14, 7));
            Assert.AreEqual(2, Action.Calculate(40, 20));

            TestWithNull(Action);

            try
            {
                Action.Calculate(100, 0);
                Assert.Fail("должна была произойти ошибка, т.к. второй аргумент 0");
            }
            catch (Exception) { }
        }
    }
}