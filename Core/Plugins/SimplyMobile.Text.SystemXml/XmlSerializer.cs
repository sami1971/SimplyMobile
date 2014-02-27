using System.IO;

namespace SimplyMobile.Text.SystemXml
{
	public class XmlSerializer : IXmlSerializer
	{
		#region ITextSerializer implementation

		public string Serialize<T>(T obj)
		{
			using (var memoryStream = new MemoryStream())
			using (var reader = new StreamReader(memoryStream))
			{
				var serializer = new System.Xml.Serialization.XmlSerializer (typeof(T)); 
				serializer.Serialize (memoryStream, obj);
				memoryStream.Position = 0;
				return reader.ReadToEnd();
			}
		}

		public T Deserialize<T>(string data)
		{
			var serializer = new System.Xml.Serialization.XmlSerializer (typeof(T)); 
			using (var reader = new StringReader(data))
			{
				return (T)serializer.Deserialize (reader);  
			}   
		}

		public Format Format
		{
			get
			{
				return Format.Xml;
			}
		}

		#endregion


	}
}

