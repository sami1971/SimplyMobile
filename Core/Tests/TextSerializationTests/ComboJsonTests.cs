using System;
using System.Collections.Generic;
#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

using SimplyMobile.Text;

namespace TextSerializationTests
{
    [TestFixture]
    public abstract class ComboJsonTests
    {
        /// <summary>
        /// Gets the serializer to use.
        /// </summary>
        protected abstract IJsonSerializer Serializer { get; }

        /// <summary>
        /// Gets the deserializer to use.
        /// </summary>
        protected abstract IJsonSerializer Deserializer { get; }

        [Test()]
        public void CanSerializePrimitive()
        {
            var p = Primitives.Create(10);
            Assert.IsTrue(TestMethods.CanSerialize(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializeDateTime()
        {
            var p = DateTime.Now;
            Assert.IsTrue(TestMethods.CanSerialize(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializeDateTimeOffset()
        {
            var p = new DateTimeOffset(DateTime.Now);
            Assert.IsTrue(TestMethods.CanSerialize(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializeDates()
        {
            var p = DateTimeDto.Create(101);
            Assert.IsTrue(TestMethods.CanSerialize(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializeSimple()
        {
            var person = new Person()
            {
                Id = 0,
                FirstName = "First",
                LastName = "Last"
            };
            Assert.IsTrue(TestMethods.CanSerialize(this.Serializer, person, this.Deserializer));
        }

        [Test()]
        public void CanSerializeInterface()
        {
            var cat = new Cat()
            {
                Name = "Just some cat"
            };

            Assert.IsTrue(TestMethods.CanSerialize<IAnimal>(this.Serializer, cat, this.Deserializer));
        }

        [Test()]
        public void CanSerializeAbstractClass()
        {
            var dog = new Dog()
            {
                Name = "GSP"
            };

            Assert.IsTrue(TestMethods.CanSerialize<Animal>(this.Serializer, dog, this.Deserializer));
        }

        [Test()]
        public void CanSerializeListWithInterfaces()
        {
            var animals = new List<IAnimal>();
            animals.Add(new Cat() { Name = "Just some cat" });
            animals.Add(new Dog() { Name = "GSP" });

            Assert.IsTrue(TestMethods.CanSerializeEnumerable(this.Serializer, animals, this.Deserializer));
        }
    }
}