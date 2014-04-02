using Android.App;
using Android.OS;
using System;
using System.Collections.Generic;
using Android.Views;
using System.Linq;
using Android.Util;

namespace SimplyMobile.Core
{
    public class ActivityCore : Activity
    {
        protected virtual IEnumerable<MenuAction> MenuActions
        {
            get
            {
                return new List<MenuAction> ();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            var app = Application.Context as MobileApp;
            if (app != null)
            {
                app.StartActivityTransition(this);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            var app = Application.Context as MobileApp;
            if (app != null && app.WasBackgrounded)
            {
                OnResumeFromBackground();
            }
        }

        protected virtual void OnResumeFromBackground()
        {
            Log.Info (this.ToString (), "Resumed from background.");
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (this.MenuActions.Any ())
            {
                int order = 0;
                foreach (var menuAction in this.MenuActions)
                {
                    var newMenu = menu.Add (0, order, order++, menuAction.Caption);
                    if (menuAction.IconId.HasValue)
                    {
                        newMenu.SetIcon (menuAction.IconId.Value);
                    }
                }

                return true;
            }

            return false;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            this.MenuActions.ElementAt(item.ItemId).Action.Invoke();
            return true;
        }
    }

    public class MenuAction
    {
        public string Caption { get; private set; }

        public Action Action { get; private set; }

        public int? IconId { get; private set; }

        public MenuAction (string caption, Action action, int? iconId = null)
        {
            this.Action = action;
            this.Caption = caption;
            this.IconId = iconId;
        }
    }
}