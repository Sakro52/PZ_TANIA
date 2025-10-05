using CalculatorLib;
using Xunit;

namespace CalculatorLibUnitTests
{
    public class CalculatorUnitTests
    {
        [Fact]
        public void TestAdding2And2()
        {
            // размещение (Arrange)
            double a = 2;
            double b = 2;
            double expected = 4;
            Calculator calc = new();

            // действие (Act)
            double actual = calc.Add(a, b);

            // утверждение (Assert)
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAdding2And3()
        {
            double a = 2;
            double b = 3;
            double expected = 5;
            Calculator calc = new();

            double actual = calc.Add(a, b);

            Assert.Equal(expected, actual);
        }
    }
}
