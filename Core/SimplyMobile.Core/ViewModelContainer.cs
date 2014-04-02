
using System;
using System.Collections.Generic;

namespace SimplyMobile.Core
{
    public static class ViewModelContainer
    {
        private static Dictionary<Guid, ViewModel> container;

        private static Dictionary<Guid, ViewModel> Container
        {
            get { return container ?? (container = new Dictionary<Guid, ViewModel>()); }
        }

        /// <summary>
        /// Get a view model.
        /// </summary>
        /// <param name="guid">GUID of the model.</param>
        /// <returns>A view model object is successful, otherwise NULL</returns>
        public static ViewModel Get(Guid guid)
        {
            return Container[guid];
        }

        public static ViewModel Get(string guid)
        {
            Guid id;
            return Guid.TryParse (guid, out id) ? Container [id] : null;
        }
        
        public static ViewModel Pull(string guid)
        {
            Guid id;
            return Guid.TryParse(guid, out id) ? Pull(id) : null;
        }

        /// <summary>
        /// Pulls a view model from the container. 
        /// 
        /// View model is removed from the container.
        /// </summary>
        /// <param name="guid">GUID of the model.</param>
        public static ViewModel Pull(Guid guid)
        {
            ViewModel model = null;
            if (Container.ContainsKey(guid))
            {
                model = Container[guid];
                Container.Remove(guid);
            }

            return model;
        }

        public static Guid Push(ViewModel model)
        {
            var guid = Guid.NewGuid();
            Container.Add(guid, model);
            return guid;
        }
    }
}
