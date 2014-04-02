using SimplyMobile.Navigation;
using SimplyMobile.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationSample
{
    public class NavigationViewModel
    {
        private INavigationController controller;
        private IWebHybrid webHybrid;

        private const string NativeCall = "openNativeView";

        public NavigationViewModel(INavigationController controller, IWebHybrid webHybrid)
        {
            this.controller = controller;
            this.webHybrid = webHybrid;
        }

        //public NavigationViewModel(NavigationDelegate<NewItemViewModel> navigationDelegate, IWebHybrid webHybrid)
        //{

        //}

        public void BindViewOwner(object owner)
        {
            this.webHybrid.RegisterCallback(NativeCall, id =>
                {
                    var newViewModel = new NewItemViewModel(id);
                    this.controller.NavigateTo(owner, newViewModel);
                });
        }

        public void UnbindViewOwner()
        {
            this.webHybrid.RemoveCallback(NativeCall);
        }
    }
}
