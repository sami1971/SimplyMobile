using System;

namespace TextSerializationTests
{
    public class Dog : Animal
    {
        #region IAnimal implementation
        public override string MakeSound ()
        {
            return "Vuf";
        }
        #endregion
    }
}

