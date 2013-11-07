using NUnit.Framework;
using System;
using SimplyMobile.Text;
using System.Diagnostics;

namespace TextSerializationTests
{
	public static class Test
	{
		public static bool CanSerialize (ITextSerializer serializer)
		{
			var person = new Person () 
			{
				Id = 0,
				FirstName = "First",
				LastName = "Last"
			};

			person.Pets.Add (new Dog () { Name = "Shorthaired German Pointer" });
			person.Pets.Add (new Cat () { Name = "Siamese" });

			var text = serializer.Serialize (person);

//			Console.WriteLine (text);

			var obj = serializer.Deserialize<Person> (text);

//			Console.WriteLine (obj);

			return (obj.Equals(person));
		}

		public static long GetSerializationSpeed(int numberOfIterations, ITextSerializer serializer)
		{
			var person = new Person () 
			{
				Id = 0,
				FirstName = "First",
				LastName = "Last"
			};

			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var n = 0; n < numberOfIterations; n++)
			{
				serializer.Serialize (person);
			}
			stopWatch.Stop ();
			return stopWatch.ElapsedMilliseconds;
		}

		public static long GetDeserializationSpeed(int numberOfIterations, ITextSerializer serializer)
		{
			var person = new Person () 
			{
				Id = 0,
				FirstName = "First",
				LastName = "Last"
			};

			var str = serializer.Serialize (person);

			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var n = 0; n < numberOfIterations; n++)
			{
				serializer.Deserialize<Person> (str);
			}
			stopWatch.Stop ();
			return stopWatch.ElapsedMilliseconds;
		}
	}
}

