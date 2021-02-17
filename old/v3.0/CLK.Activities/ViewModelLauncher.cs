using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Activities
{
    public interface ViewModelLauncher
    {
        // Properties
        Uri ViewUri { get; }


        // Methods
        ViewModel Create(Uri uri, Dictionary<string, object> bundle = null);
    }

    public abstract class ViewModelLauncher<TViewModel> : ViewModelLauncher
        where TViewModel : ViewModel
    {
        // Constructors
        public ViewModelLauncher(string activityUri = null)
        {
            // ViewUri
            if(string.IsNullOrEmpty(activityUri)==true)
            {
                activityUri = string.Format("{0}://{1}/{2}", "shell", typeof(TViewModel).Namespace, typeof(TViewModel).Name);
            }
            this.ViewUri = new Uri(activityUri.Replace("*", ""));
        }


        // Properties
        public Uri ViewUri { get; private set; }


        // Methods
        public abstract ViewModel Create(Uri uri, Dictionary<string, object> bundle = null);
    }
}
