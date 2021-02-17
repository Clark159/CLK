using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLK.Activities
{
    public class ShellModel : ViewModel
    {
        // Fields
        private readonly Uri _homeUri = null;

        private ViewModel _viewModel = null;


        // Constructors
        public ShellModel(string homeUri = null)
        {
            // HomeUri
            if (string.IsNullOrEmpty(homeUri) == false)
            {
                _homeUri = new Uri(homeUri);
            }
            else
            {
                _homeUri = this.ViewModelLauncherList.FirstOrDefault()?.ViewUri;
            }

            // Commands
            this.NavigateCommand = new RelayCommand<Uri>((uri) => this.Navigate(uri));
            this.NavigateHomeCommand = new RelayCommand(() => this.NavigateHome());
        }

        public override void Start()
        {
            // Base
            base.Start();

            // Home
            this.NavigateHome();
        }

        public override void Dispose()
        {
            // Base
            base.Dispose();
        }


        // Properties
        public virtual ViewModel ViewModel
        {
            get { return _viewModel; }
            private set
            {
                // Detach
                if (_viewModel != null)
                {
                    this.Detach(_viewModel);
                }

                // Attach
                if (value != null)
                {
                    this.Attach(value);
                }

                // SetValue
                this.SetValue(ref _viewModel, value);
            }
        }

        public virtual IEnumerable<ViewModelLauncher> ViewModelLauncherList
        {
            get { return ActivityContext.Current.ViewModelLauncherList.Where((viewModelLauncher) => viewModelLauncher is ActivityModel).Cast<ViewModelLauncher<ActivityModel>>(); }
        }


        // Commands
        public ICommand NavigateCommand { get; private set; }

        public ICommand NavigateHomeCommand { get; private set; }


        // Methods
        public void Navigate(Uri uri, Dictionary<string, object> bundle = null)
        {
            #region Contracts

            if (uri == null) throw new ArgumentException();

            #endregion

            // Navigate
            this.ViewModel = ActivityContext.Current.CreateViewModel(uri, bundle);
        }

        public void NavigateHome()
        {
            // Require
            if (_homeUri == null) return;

            // Navigate
            this.Navigate(_homeUri, null);
        }


        protected virtual void Attach(ViewModel viewModel)
        {
            #region Contracts

            if (viewModel == null) throw new ArgumentException();

            #endregion

            // ActivityModel
            if (viewModel is ActivityModel) this.Attach(viewModel as ActivityModel);
        }

        protected virtual void Detach(ViewModel viewModel)
        {
            #region Contracts

            if (viewModel == null) throw new ArgumentException();

            #endregion

            // ActivityModel
            if (viewModel is ActivityModel) this.Detach(viewModel as ActivityModel);
        }

        private void Attach(ActivityModel activityModel)
        {
            #region Contracts

            if (activityModel == null) throw new ArgumentException();

            #endregion

            // Attach
            activityModel.HomeNavigated += this.ActivityModel_HomeNavigated;
            activityModel.Navigated += this.ActivityModel_Navigated;
        }

        private void Detach(ActivityModel activityModel)
        {
            #region Contracts

            if (activityModel == null) throw new ArgumentException();

            #endregion

            // Detach
            activityModel.HomeNavigated -= this.ActivityModel_HomeNavigated;
            activityModel.Navigated -= this.ActivityModel_Navigated;
        }


        // Handlers
        private void ActivityModel_Navigated(Uri uri, Dictionary<string, object> bundle = null)
        {
            #region Contracts

            if (uri == null) throw new ArgumentException();

            #endregion

            // Navigate
            this.Navigate(uri, bundle);
        }

        private void ActivityModel_HomeNavigated()
        {
            // Navigate
            this.NavigateHome();
        }
    }
}
