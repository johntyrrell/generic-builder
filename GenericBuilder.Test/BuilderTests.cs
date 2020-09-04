using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericBuilder.Test
{
    [TestClass]
    public class BuilderTests
    {
        [TestMethod]
        public void Given_Simple_Properties_And_Action_When_Build_Then_Amount_Has_Correct_Length()
        {
            var test = new Builder<Test>()
                .SetAction(t => t.Name, t => t.Amount = t.Name.Length)
                .With(t => t.Amount, 123)
                .With(t => t.Date, new DateTime(1212, 12, 22))
                .With(t => t.Name, "Yo")
                .Build();

            Assert.AreEqual(2, test.Amount);
        }

        [TestMethod]
        public void Given_CustomTestBuilder_With_Custom_Function_When_Build_Then_Date_Should_Not_Have_Default_Date()
        {
            var test = new CustomTestBuilder()
                .WithNameAndAmount("Yo!", 999)
                .Build();

            Assert.AreEqual(new DateTime(2000, 1, 1), test.Date);
        }
    }
}
