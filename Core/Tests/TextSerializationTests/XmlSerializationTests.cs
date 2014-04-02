using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace TextSerializationTests
{
    public class BusinessOffer
    {
    }

    [TestFixture()]
    public class XmlSerializationTests
    {
        private const string XmlSample = 
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<ArrayOfBusinessOffer xmlns:xsi=\"w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"w3.org/2001/XMLSchema\">" +
            "<BusinessOffer></BusinessOffer></ArrayOfBusinessOffer>";

        [Test()]
        public void XmlSampleTest()
        {
            var serializer = new System.Xml.Serialization.XmlSerializer (typeof(List<BusinessOffer>)); 

            object list;

            using (var reader = new StringReader(XmlSample))
            {
                list = serializer.Deserialize (reader);  
            }   

//          var serializer = new XmlSerializer ();
//
//          var list = serializer.Deserialize<List<BusinessOffer>> (XmlSample);
//
//          if (list != null)
//          {
//
//          }

            Assert.IsNotNull (list);
        }
    }
}

