using System;
using SimplyMobile.Text;

namespace TextSerializationTests
{
    public class FastJsonList : ListSerializationSpeed
    {
        protected override ITextSerializer Serializer 
        { 
            get 
            { 
                return new SimplyMobile.Text.FastJson.JsonSerializer(new SimplyMobile.Text.ServiceStack.JsonSerializer()); 
            } 
        }
    }
}

