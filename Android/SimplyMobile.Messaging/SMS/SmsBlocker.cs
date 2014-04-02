using System;
using Android.Content;

namespace SimplyMobile.Messaging
{
    using Core;

    public class SmsBlocker : Monitor
    {
        #region implemented abstract members of BroadcastReceiver

        public override void OnReceive (Context context, Intent intent)
        {
            foreach (var i in intent.Extras.KeySet())
            {
                System.Diagnostics.Debug.WriteLine (i);
            }
        }

        #endregion

        #region implemented abstract members of Monitor

        protected override IntentFilter Filter
        {
            get
            {
                return new IntentFilter("android.provider.Telephony.SMS_RECEIVED")
                { 
                    Priority = ( int )IntentFilterPriority.HighPriority 
                };
            }
        }

        #endregion

        protected void Block()
        {
            this.InvokeAbortBroadcast();
        }

        public SmsBlocker ()
        {
        }
    }
}

