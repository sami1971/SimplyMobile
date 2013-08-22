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

namespace SimplyMobile.Core
{
    public abstract class Monitor
    {
        private bool active;

        /// <summary>
        ///  Occurs on monitor status change. 
        /// </summary>
        public event EventHandler<EventArgs<bool>> OnActiveChanged;

        public event EventHandler<EventArgs<Exception>> OnException;

        /// <summary>
        /// Gets a value indicating whether this <see cref="SimplyMobile.Core.IMonitor"/> is active.
        /// </summary>
        /// <value>
        /// <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Active
        {
            get { return active; }
            protected set
            {
                if (active == value) return;

                active = value;
                if (this.OnActiveChanged != null)
                {
                    this.OnActiveChanged(this, new EventArgs<bool>(active));
                }
            }
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