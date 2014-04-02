using System;

namespace TextSerializationTests
{
    public class Cat : Animal
    {
        #region IAnimal implementation
        public override string MakeSound()
        {
            return "Miau";
        }
        #endregion


    }
}

