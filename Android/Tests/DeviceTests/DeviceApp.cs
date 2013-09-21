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
using SimplyMobile.Core;
using Android.Runtime;
using Android.App;

namespace DeviceTests
{
	[Application(Debuggable = true,
	             Label = "DeviceApp",
	             ManageSpaceActivity = typeof(Activity))]
	public partial class DeviceApp
	{
	    /// <summary>
	    /// Initializes a new instance of the <see cref="DeviceApp"/> class.
	    /// </summary>
	    /// <param name="javaReference">
	    /// The java reference.
	    /// </param>
	    /// <param name="transfer">
	    /// The transfer.
	    /// </param>
	    public DeviceApp(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{

		}

	    /// <summary>
	    /// The on create.
	    /// </summary>
	    public override void OnCreate ()
		{
			base.OnCreate ();
		}

        public override void OnTerminate()
        {
            base.OnTerminate();
        }
	}
}

