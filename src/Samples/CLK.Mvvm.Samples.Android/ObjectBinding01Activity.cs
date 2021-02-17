using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CLK.Mvvm.Samples.Android
{
    [Activity(Label = "ObjectBinding01Activity")]
    public class ObjectBinding01Activity : Activity
    {
        // Fields
        private MainViewModel _viewModel;

        private EditText _editText1;

        private Button _button1;


        // Properties
        public MainViewModel ViewModel
        {
            get
            {
                return _viewModel ?? (_viewModel = new MainViewModel());
            }
        }

        public EditText EditText1
        {
            get
            {
                return _editText1 ?? (_editText1 = this.FindViewById<EditText>(Resource.Id.EditText1));
            }
        }

        public Button Button1
        {
            get
            {
                return _button1 ?? (_button1 = this.FindViewById<Button>(Resource.Id.Button1));
            }
        }


        // Methods
        protected override void OnCreate(Bundle bundle)
        {
            // Base
            base.OnCreate(bundle);

            // ActionBar
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Standard;
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);

            // SetContent
            this.SetContentView(Resource.Layout.ObjectBinding01View);
            this.SetContentBinding();
            this.SetContentEvent(); 
        }

        private void SetContentBinding()
        {
            // Binding
            CLK.Mvvm.BindingContext.AddBinding(() => this.ViewModel.Data, () => this.EditText1.Text, BindingMode.TwoWay);
        }

        private void SetContentEvent()
        {
            // Events
            this.Button1.Click += this.Button1_Click;
        }

        
        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            #region Contracts

            if (item == null) throw new ArgumentNullException();

            #endregion

            // Distribute
            switch (item.ItemId)
            {
                // Home
                case global::Android.Resource.Id.Home:
                    this.OnBackPressed();
                    break;
            }

            // Base
            return base.OnMenuItemSelected(featureId, item);
        }


        // Handlers
        private void Button1_Click(object sender, EventArgs e)
        {
            this.ViewModel.Update();
        }        
    }
}