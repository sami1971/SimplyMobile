using SimplyMobile.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationSample
{
    public class NewItemViewModel : ViewModel
    {
        public NewItemViewModel(string id)
        {
            this.Id = id;
        }

        public string Id { get; private set; }
    }
}
