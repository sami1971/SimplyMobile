//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using System;
using System.Collections.Generic;
using SimplyMobile.Text;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace TextSerializationTests
{
    [TestFixture ()]
    public abstract class SanityCheckTests
    {
        /// <summary>
        /// Gets the serializer.
        /// </summary>
        protected abstract ITextSerializer Serializer { get; }

        /// <summary>
        /// Gets the deserializer.
        /// </summary>
        protected abstract ITextSerializer Deserializer { get; }

        [Test()]
        public void CanSerializePrimitive()
        {
            var p = Primitives.Create (10);
            Assert.IsTrue(TestMethods.CanSerialize<Primitives>(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializePrimitiveList()
        {
            var list = new PrimitiveList ();

            for (var n = 0; n < 10; n++)
            {
                list.List.Add(Primitives.Create(n));
            }

            Assert.IsTrue(TestMethods.CanSerialize<PrimitiveList>(this.Serializer, list, this.Serializer));
        }

        [Test()]
        public void CanSerializeDateTime()
        {
            var p = DateTime.Now;
            Assert.IsTrue(TestMethods.CanSerialize<DateTime>(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializeDateTimeOffset()
        {
            var p = new DateTimeOffset(DateTime.Now);
            Assert.IsTrue(TestMethods.CanSerialize<DateTimeOffset>(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializeDates()
        {
            var p = DateTimeDto.Create(101);
            Assert.IsTrue(TestMethods.CanSerialize<DateTimeDto>(this.Serializer, p, this.Deserializer));
        }

        [Test()]
        public void CanSerializeSimple ()
        {
            var person = new Person () 
            {
                Id = 0,
                FirstName = "First",
                LastName = "Last"
            };
            Assert.IsTrue(TestMethods.CanSerialize<Person>(this.Serializer, person, this.Deserializer));
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
            var animals = new List<IAnimal> { new Cat() { Name = "Just some cat" }, new Dog() { Name = "GSP" } };

            Assert.IsTrue(TestMethods.CanSerializeEnumerable(this.Serializer, animals, this.Deserializer));
        }
    }

    public class PrimitiveList
    {
        public PrimitiveList()
        {
            List = new List<Primitives>();
        }

        public List<Primitives> List { get; set;}
    }
}

