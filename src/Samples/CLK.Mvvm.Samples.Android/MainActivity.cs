using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CLK.Mvvm.Samples.Android
{
    [Activity(Label = "CLK.Mvvm", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        // Fields
        private Button _button1;

        private Button _button2;

        private Button _button3;


        // Properties
        public Button Button1
        {
            get
            {
                return _button1 ?? (_button1 = this.FindViewById<Button>(Resource.Id.Button1));
            }
        }

        public Button Button2
        {
            get
            {
                return _button2 ?? (_button2 = this.FindViewById<Button>(Resource.Id.Button2));
            }
        }

        public Button Button3
        {
            get
            {
                return _button3 ?? (_button3 = this.FindViewById<Button>(Resource.Id.Button3));
            }
        }


        // Methods
        protected override void OnCreate(Bundle bundle)
        {
            // Base
            base.OnCreate(bundle);

            // ActionBar
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Standard;
            this.ActionBar.SetDisplayHomeAsUpEnabled(false);

            // SetContent
            this.SetContentView(Resource.Layout.MainView);
            this.SetContentBinding();
            this.SetContentEvent();             
        }

        private void SetContentBinding()
        {
            
        }

        private void SetContentEvent()
        {
            // Events
            this.Button1.Click += this.Button1_Click;
            this.Button2.Click += this.Button2_Click;
            this.Button3.Click += this.Button3_Click;
        }


        // Handlers
        private void Button1_Click(object sender, EventArgs e)
        {
           this.StartActivity(typeof(ObjectBinding01Activity));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.StartActivity(typeof(ObjectBinding02Activity));
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.StartActivity(typeof(ObjectBinding03Activity));
        }
    }
}

