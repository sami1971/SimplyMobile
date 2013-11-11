using System;
using System.Diagnostics;
using SimplyMobile.Text;

namespace TextSerializationTests
{
	public static class TestMethods
	{
		public static bool CanSerialize<T> (ITextSerializer serializer, T item)
		{
//			person.Pets.Add (new Dog () { Name = "Shorthaired German Pointer" });
//			person.Pets.Add (new Cat () { Name = "Siamese" });

			var text = serializer.Serialize (item);

			Console.WriteLine (text);

			var obj = serializer.Deserialize<T> (text);

			Console.WriteLine (obj);

			return (obj.Equals(item));
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

		public static long GetSerializationSpeed(int numberOfIterations, ITextSerializer serializer, object o, out string text)
		{
			text = string.Empty;

			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var n = 0; n < numberOfIterations; n++)
			{
				text = serializer.Serialize (o);
			}
			stopWatch.Stop ();
			return stopWatch.ElapsedMilliseconds;
		}

		public static long GetDeserializationSpeed<T>(int numberOfIterations, ITextSerializer serializer, string text, out T deserialized)
		{
			deserialized = default(T);

			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var n = 0; n < numberOfIterations; n++)
			{
				deserialized = serializer.Deserialize<T> (text);
			}
			stopWatch.Stop ();
			return stopWatch.ElapsedMilliseconds;
		}
	}
}

