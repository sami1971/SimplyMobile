using System;

namespace TextSerializationTests
{
	public interface IAnimal : IEquatable<IAnimal>
	{
		string Name { get; set; }

		string MakeSound();
	}
}

