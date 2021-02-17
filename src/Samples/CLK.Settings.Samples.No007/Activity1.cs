using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CLK.Settings.Samples.No007
{
    [Activity(Label = "CLK.Settings.Samples.No007", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            // Base
            base.OnCreate(bundle);

            // View
            SetContentView(Resource.Layout.Main);

            try
            {
                // Context
                var settingContext = new IsolatedStorageSettingContext();

                // Default
                if (settingContext.AppSettings.ContainsKey("count") == true)
                {
                    count = int.Parse(settingContext.AppSettings["count"]);
                }

                // Test
                Button button = FindViewById<Button>(Resource.Id.MyButton);
                button.Click += delegate
                {
                    count++;
                    button.Text = string.Format("{0} clicks!", count.ToString());
                    settingContext.AppSettings["count"] = count.ToString();
                };
            }
            catch (Exception ex)
            {
                Button button = FindViewById<Button>(Resource.Id.MyButton);
                button.Text = ex.Message;
            }
        }
    }
}

