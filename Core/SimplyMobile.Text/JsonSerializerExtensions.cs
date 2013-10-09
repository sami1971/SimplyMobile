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
using System.IO;

namespace SimplyMobile.Text
{
	/// <summary>
	/// Json serializer extensions.
	/// </summary>
	public static class JsonSerializerExtensions
	{
		/// <summary>
		/// Serializes to stream.
		/// </summary>
		/// <param name="obj">Object to serialize.</param>
		/// <param name="stream">Stream.</param>
		public static void SerializeToStream (this IJsonSerializer serializer, object obj, Stream stream)
		{
			using (var streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write (serializer.Serialize (obj));
			}
		}

		/// <summary>
		/// Deserializes from stream.
		/// </summary>
		/// <returns>The from stream.</returns>
		/// <param name="stream">Stream.</param>
		/// <typeparam name="T">The type of object to deserialize.</typeparam>
		public static T DeserializeFromStream<T>(this IJsonSerializer serializer, Stream stream)
		{
			using (var streamReader = new StreamReader(stream))
			{
				var text = streamReader.ReadToEnd();
				return serializer.Deserialize<T>(text);
			}
		}

		/// <summary>
		/// Serializes to writer.
		/// </summary>
		/// <param name="obj">Object to serialize.</param>
		/// <param name="writer">Writer.</param>
		public static void SerializeToWriter(this IJsonSerializer serializer, object obj, TextWriter writer)
		{
			writer.Write (serializer.Serialize (obj));
		}

		/// <summary>
		/// Deserializes from reader.
		/// </summary>
		/// <returns>The serialized object from reader.</returns>
		/// <param name="reader">Reader to deserialize from.</param>
		public static T DeserializeFromReader<T>(this IJsonSerializer serializer, TextReader reader)
		{
			return serializer.Deserialize<T> (reader.ReadToEnd ());
		}
	}
}

