using System;

namespace TextSerializationTests
{
    public class ServiceStackList : ListSerializationSpeed
    {
        #region implemented abstract members of ListSerializationSpeed

        protected override SimplyMobile.Text.ITextSerializer Serializer {
            get
            {
                return new SimplyMobile.Text.ServiceStack.JsonSerializer ();
            }
        }

        #endregion


    }
}

