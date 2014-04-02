using System;
using SimplyMobile.Web;

namespace SimplyMobile.Web.CanvasJs
{
    public partial class CanvasView : WebHybrid
    {
        public void SetModel(Model model)
        {
            this.CallJsFunction("loadModel", model);
        }
    }
}

