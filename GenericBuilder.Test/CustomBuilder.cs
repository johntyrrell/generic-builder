using System;

namespace GenericBuilder.Test
{
    public class CustomTestBuilder : Builder<Test>
    {
        public CustomTestBuilder()
        {
            ObjectToBuild.Name = "Default";
            ObjectToBuild.Amount = 1200;
            ObjectToBuild.Date = new DateTime(2020, 9, 4);

            SetAction(p => p.Name, t => t.Date = new DateTime(2000, 1, 1));
        }

        public CustomTestBuilder WithNameAndAmount(string name, int amount)
        {
            With(p => p.Name, name);
            With(p => p.Amount, amount);

            return this;
        }
    }
}
