using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Activities
{
    public interface ViewFactory
    {
        // Properties
        Type ModelType { get; }


        // Methods
        View Create();
    }

    public class ViewFactory<TView, TViewModel> : ViewFactory
        where TView : View
        where TViewModel : ViewModel
    {
        // Fields
        private readonly Func<TView> _createAction = null;


        // Constructors
        public ViewFactory(Func<TView> createAction)
        {
            #region Contracts

            if (createAction == null) throw new ArgumentNullException();

            #endregion

            // Default
            _createAction = createAction;
        }


        // Properties
        public Type ModelType { get { return typeof(TViewModel); } }


        // Methods
        public View Create()
        {
            //Create
            return _createAction();
        }
    }
}
