
using Rhino.Mocks;
using System;
using NUnit.Framework;
using FilmScanner;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace FilmScanner.Test
{

    [TestFixture]
    public class RhinoSample : TestBase
    {

        [Test]
        public void RhinoBasic()
        {
            // Arrange
            var foo = MockRepository.GenerateStub<ICalculator>();
            foo.Stub(x => x.Add(1, 4)).Return(5);
            foo.Stub(x => x.Add(2, 3)).Return(5);

            // Act
            var result = foo.Add(2, 3);

            // Assert
            Assert.That(result, Is.EqualTo(5));
        }


        [Test]
        public void Calc_Test()
        {
            // Arrange
            var foo = MockRepository.GenerateStub<ICalculator>();
            foo.Stub(x => x.Add(1, 4)).Return(5);
            foo.Stub(x => x.Add(2, 3)).Return(5);

            // Act
            var result = foo.Add(2, 3);
            var cut = new ClassUnderTest(foo);

            Assert.That(cut.Calc(1, 4), Is.EqualTo(5));

            // Assert
            Assert.That(result, Is.EqualTo(5));
        }

    }

}


public class ClassUnderTest
{
    public ICalculator Calculator;

    public ClassUnderTest(ICalculator calc)
    {
        this.Calculator = calc;
    }

    public int Calc(int param1, int param2)
    {
        return this.Calculator.Add(param1, param2);
    }

}


public class Calculator : ICalculator
{

    public int Add(int param1, int param2)
    {
        throw new NotImplementedException();
        //return param1 + param2;
    }

}


public interface ICalculator
{
    int Add(int param1, int param2);
}
