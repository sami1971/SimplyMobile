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
using System.Collections.Generic;
using System.Linq;

namespace SimplyMobile.Core
{
    public class DependencyResolver : IDependencyResolver
    {
        private static IDependencyResolver instance;
        private readonly List<object> services;

        public DependencyResolver()
        {
            this.services = new List<object>();
        }

        public T GetService<T>() where T : class
        {
            return this.services.OfType<T>().FirstOrDefault();
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            return this.services.OfType<T>();
        }

        public void SetService<T>(T service) where T : class
        {
            this.services.Add(service);
        }

        public static IDependencyResolver Current
        {
            get { return instance ?? (instance = new DependencyResolver()); }
            set { instance = value; }
        }
    }
}