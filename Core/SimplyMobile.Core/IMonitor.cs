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
    /// <summary>
    /// Monitor interface
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// Event handler for active status changes
        /// </summary>
        event EventHandler<EventArgs<bool>> OnActiveChanged;

        /// <summary>
        /// Event handler for exceptions
        /// </summary>
        event EventHandler<EventArgs<Exception>> OnException;
 
        /// <summary>
        /// Gets a value indicating whether the monitor is active
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// Start monitoring.
        /// </summary>
        /// <returns>True when monitor starts, otherwise false</returns>
        bool Start();

        /// <summary>
        /// Stop monitoring.
        /// </summary>
        void Stop();
    }
}