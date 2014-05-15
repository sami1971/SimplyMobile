using System;
using SimplyMobile.Core;

namespace SimplyMobile
{
    using Navigation;

    public class ViewModelActivity<T> : ActivityCore where T : ViewModel
    {
        private const string ModelId = "modelId";

        /// <summary>
        /// The ViewModel for this activity
        /// </summary>
        protected T Model;

        /// <summary>
        /// Raises the create event and pulls the ViewModel from the container.
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            this.Model = ViewModelContainer.Pull ((savedInstanceState ?? this.Intent.Extras).GetString (ModelId)) as T;

            var activityModel = this.Model as NavigatorViewModel;

            if (activityModel != null)
            {
                activityModel.Presenter = this;
            }
        }

        /// <Docs>Bundle in which to place your saved state.</Docs>
        /// <summary>
        /// Raises the save instance state event and puts the ViewModel back into the container.
        /// </summary>
        /// <param name="outState">Out state.</param>
        protected override void OnSaveInstanceState(Android.OS.Bundle outState)
        {
            var guid = ViewModelContainer.Push (this.Model);
            outState.PutString (ModelId, guid.ToString ());
            base.OnSaveInstanceState (outState);
        }
    }
}

