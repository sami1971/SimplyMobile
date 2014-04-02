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
using Android.App;
using Android.Content;

namespace SimplyMobile.Core
{
    public abstract class Monitor : BroadcastReceiver, IMonitor
    {
        private bool _active;

        /// <summary>
        ///  Occurs on monitor status change. 
        /// </summary>
        public event EventHandler<EventArgs<bool>> OnActiveChanged;

        public event EventHandler<EventArgs<Exception>> OnException;

        protected abstract IntentFilter Filter { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SimplyMobile.Core.IMonitor"/> is active.
        /// </summary>
        /// <value>
        /// <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Active
        {
            get { return _active; }
            protected set
            {
                if (_active == value) return;

                _active = value;
                if (this.OnActiveChanged != null)
                {
                    this.OnActiveChanged(this, new EventArgs<bool>(_active));
                }
            }
        }

        /// <summary>
        ///  Start monitoring. 
        /// </summary>
        public virtual bool Start()
        {
            var intent = this.RegisterReceiver(this, this.Filter);
            if (intent == null)
            {
                this.ReportException(new Exception("Intent is NULL"));
                return this.Active = false;
            }

            return this.Active = true;
        }

        /// <summary>
        ///  Stop monitoring. 
        /// </summary>
        public virtual void Stop()
        {
            this.UnregisterReceiver();
            this.Active = false;
        }

        protected void ReportException(Exception exception)
        {
            if (this.OnException != null)
            {
                this.OnException(this, new EventArgs<Exception>(exception));
            }
        }
    }
}