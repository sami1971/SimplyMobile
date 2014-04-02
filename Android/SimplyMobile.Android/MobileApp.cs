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
using Android.Runtime;

namespace SimplyMobile.Core
{
    public partial class MobileApp : Application
    {
        private static DateTime activityTransitionTime;

        /// <summary>
        /// Gets maximum time in ms for activity to transit before application 
        /// is considered to be backgrounded
        /// </summary>
        protected virtual long MaximumActivityTransitionTime
        {
            get { return 2000; }
        }

        /// <summary>
        /// Gets a value indicating whether application was backgrounded.
        /// </summary>
        public bool WasBackgrounded
        {
            get
            {
                var ret = (DateTime.UtcNow - activityTransitionTime).TotalMilliseconds > this.MaximumActivityTransitionTime;

                if (ret)
                {
                    this.OnResumeFromBackground();
                }

                return ret;
            }
        }

        public void StartActivityTransition(Activity activity)
        {
            activityTransitionTime = DateTime.UtcNow;
        }

        public MobileApp(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
        {
            
        }

        public override void OnCreate ()
        {
            base.OnCreate ();
//          DependencyResolver.Current = Resolver;
//          Resolver.SetService(this);
            activityTransitionTime = DateTime.UtcNow;
        }

        protected virtual void OnResumeFromBackground()
        {
            
        }
    }
}