using System;

namespace TextSerializationTests
{
    public class JsonNetList : ListSerializationSpeed
    {
        #region implemented abstract members of ListSerializationSpeed

        protected override SimplyMobile.Text.ITextSerializer Serializer {
            get
            {
                return new SimplyMobile.Text.JsonNet.JsonSerializer ();
            }
        }

        #endregion


    }
}

