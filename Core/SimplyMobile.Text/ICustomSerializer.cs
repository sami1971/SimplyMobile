using System;

namespace SimplyMobile.Text
{
    public interface ICustomSerializer<T> : ITextSerializer
    {
        /// <summary>
        /// Serializes object to a string
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Serialized string of the object</returns>
        string Serialize(T obj);

        /// <summary>
        /// Deserializes string into an object
        /// </summary>
        /// <typeparam name="T">Type of object to serialize to</typeparam>
        /// <param name="data">Serialized object</param>
        /// <returns>Object of type T</returns>
        T Deserialize(string data);
    }
}

