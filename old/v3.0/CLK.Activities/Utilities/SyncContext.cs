using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CLK.Activities
{
    public interface SyncContext
    {
        /// <summary>
        ///  在衍生類別中覆寫時，會將非同步訊息分派至同步處理內容。
        /// </summary>
        /// <param name="d">要呼叫的 System.Threading.SendOrPostCallback 委派。</param>
        /// <param name="state">傳送至委派的物件。</param>
        void Post(SendOrPostCallback d, object state);

        /// <summary>
        ///  在衍生類別中覆寫時，會將同步訊息分派至同步處理內容。
        /// </summary>
        /// <param name="d">要呼叫的 System.Threading.SendOrPostCallback 委派。</param>
        /// <param name="state">傳送至委派的物件。</param>
        void Send(SendOrPostCallback d, object state);
    }

    public class WpfSyncContext : SyncContext
    {
        // Fields
        private readonly Dispatcher _dispatcher = null;


        // Constructors
        public WpfSyncContext(Dispatcher dispatcher)
        {
            #region Contracts

            if (dispatcher == null) throw new ArgumentException();

            #endregion

            // Default
            _dispatcher = dispatcher;
        }


        // Methods
        public void Post(SendOrPostCallback d, object state)
        {
            #region Contracts

            if (d == null) throw new ArgumentException();

            #endregion

            // BeginInvoke
            _dispatcher.BeginInvoke(d, DispatcherPriority.Input, state);
        }

        public void Post(SendOrPostCallback d, DispatcherPriority priority, object state)
        {
            #region Contracts

            if (d == null) throw new ArgumentException();

            #endregion

            // BeginInvoke
            _dispatcher.BeginInvoke(d, priority, state);
        }


        public void Send(SendOrPostCallback d, object state)
        {
            // Invoke
            _dispatcher.Invoke(d, DispatcherPriority.Input, state);
        }

        public void Send(SendOrPostCallback d, DispatcherPriority priority, object state)
        {
            // Invoke
            _dispatcher.Invoke(d, priority, state);
        }
    }
}
