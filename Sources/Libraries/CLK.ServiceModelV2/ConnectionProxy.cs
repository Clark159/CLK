﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CLK.ServiceModel
{
    public class ConnectionProxy<TService> : ConnectionProxy
        where TService : class, IConnectionService
    {
        // Fields
        private readonly object _syncRoot = new object();

        private int _heartbeatInterval = 5000;

        private int _reconnectInterval = 5000;


        private bool _isClosed = true;

        private readonly ManualResetEvent _isClosedEvent = new ManualResetEvent(true);


        private Thread _executeThread = null;

        private readonly ManualResetEvent _executeThreadEvent = new ManualResetEvent(true);

        private readonly AutoResetEvent _executeTriggerEvent = new AutoResetEvent(false);


        private readonly ChannelFactory<TService> _channelFactory = null;

        private IContextChannel _channel = null;

        private TService _service = null;

        private bool _isConnected = false;


        // Constructors
        public ConnectionProxy(Binding binding, EndpointAddress adress) : this(new ChannelFactory<TService>(binding, adress)) 
        {
            #region Contracts

            if (binding == null) throw new ArgumentNullException();
            if (adress == null) throw new ArgumentNullException();

            #endregion

        }

        internal ConnectionProxy(ChannelFactory<TService> channelFactory)
        {
            #region Contracts

            if (channelFactory == null) throw new ArgumentNullException();

            #endregion

            // ChannelFactory     
            _channelFactory = channelFactory;
        }


        // Properties        
        public int HeartbeatInterval
        {
            get
            {
                return _heartbeatInterval;
            }
            set
            {
                if (0 >= value) value = Timeout.Infinite;
                _heartbeatInterval = value;
            }
        }

        public int ReconnectInterval
        {
            get
            {
                return _reconnectInterval;
            }
            set
            {
                if (0 >= value) value = Timeout.Infinite;
                _reconnectInterval = value;
            }
        }

        public override bool IsConnected
        {
            get
            {
                lock (_syncRoot)
                {
                    // Return
                    return _isConnected;
                }
            }
        }       

        public TService Service
        {
            get
            {
                // Get
                TService service = _service;
                if (service == null) throw new CommunicationException();

                // Return
                return service;
            }
        }


        // Methods
        public override void Open()
        {
            // Require
            lock (_syncRoot)
            {
                if (_isClosed == false) return;
                _isClosed = false;
                _isClosedEvent.Reset();
            }

            // ChannelFactory 
            _channelFactory.Open();

            // ExecuteThread
            _executeThreadEvent.Reset();
            _executeThread = new Thread(this.ExecuteOperation);
            _executeThread.IsBackground = false;
            _executeThread.Start();
        }

        public override void Close()
        {
            // Require
            lock (_syncRoot)
            {
                if (_isClosed == true) return;
                _isClosed = true;
                _isClosedEvent.Set();
            }

            // ExecuteThread
            _executeThreadEvent.WaitOne();

            // ChannelFactory 
            try
            {
                _channelFactory.Close();
            }
            catch 
            {
                _channelFactory.Abort();
            }
        }


        private void ExecuteOperation()
        {
            try
            {
                // WaitHandles
                WaitHandle[] waitHandles = new WaitHandle[]
                {
                    _isClosedEvent,
                    _executeTriggerEvent,
                };

                // Execute
                while (true)
                {
                    // Require
                    if (_isClosed == true) return;

                    // Execute       
                    this.ExecuteOperation_Connect();
                    this.ExecuteOperation_Heartbeat();
                    this.ExecuteOperation_Disconnect();

                    // Wait
                    if (_channel == null)
                    {
                        WaitHandle.WaitAny(waitHandles, this.ReconnectInterval);
                    }
                    else
                    {
                        WaitHandle.WaitAny(waitHandles, this.HeartbeatInterval);
                    }
                }
            }
            catch (Exception ex)
            {
                // Fail
                Debug.Fail(string.Format("Action:{0}, State:{1}, Message:{2}", "ExecuteOperation", "Exception", ex.Message));
            }
            finally
            {
                // End
                _executeThreadEvent.Set();
            }
        }

        private void ExecuteOperation_Connect()
        {
            // Require
            if (_service != null) return;
            if (_channel != null) return;

            // Result
            bool executeResult = false;

            // Connect
            try
            {
                // Create
                object channelObject = _channelFactory.CreateChannel();
                if (channelObject == null) throw new InvalidOperationException();
                if ((channelObject is TService) == false) throw new InvalidOperationException();
                if ((channelObject is IContextChannel) == false) throw new InvalidOperationException();

                // Service
                _service = channelObject as TService;

                // Channel
                _channel = channelObject as IContextChannel;
                _channel.Closed += this.Channel_Closed;
                _channel.Faulted += this.Channel_Faulted;
                _channel.Open();

                // Result
                executeResult = true;
            }
            catch 
            {
                // Result
                executeResult = false;
            }

            // IsConnected
            if (executeResult == true)
            {
                lock (_syncRoot)
                {
                    if (_isConnected == true) return;
                    _isConnected = true;
                }
            }

            // Notify
            if (executeResult == true)
            {
                this.OnConnected();
            }
        }

        private void ExecuteOperation_Disconnect()
        {
            // Require
            if (_service == null) return;
            if (_channel == null) return;
            if (_channel.State == CommunicationState.Opened && _isClosed == false) return;

            // Result
            bool executeResult = false;

            // Disconnect
            try
            {
                // Service
                _service = null;

                // Channel
                _channel.Close();
                _channel.Closed -= this.Channel_Closed;
                _channel.Faulted -= this.Channel_Faulted;
                _channel = null;
            }
            catch 
            {
                // Service
                _service = null;

                // Channel
                _channel.Abort();
                _channel.Closed -= this.Channel_Closed;
                _channel.Faulted -= this.Channel_Faulted;
                _channel = null;
            }
            finally
            {
                // Result
                executeResult = true;
            }

            // IsConnected
            if (executeResult == true)
            {
                lock (_syncRoot)
                {
                    if (_isConnected == false) return;
                    _isConnected = false;
                }
            }

            // Notify
            if (executeResult == true)
            {
                this.OnDisconnected();
            }
        }

        private void ExecuteOperation_Heartbeat()
        {
            // Require
            if (_service == null) return;
            if (_channel == null) return;
            if (_channel.State != CommunicationState.Opened) return;

            // Result
            bool executeResult = false;

            // Heartbeat            
            try
            {
                // Service
                _service.Heartbeat();

                // Result
                executeResult = true;
            }
            catch 
            {
                // Result
                executeResult = false;
            }

            // Notify
            if (executeResult == true)
            {
                this.OnHeartbeating();
            }
        }


        // Handlers
        private void Channel_Closed(object sender, EventArgs e)
        {
            // Trigger
            _executeTriggerEvent.Set();
        }

        private void Channel_Faulted(object sender, EventArgs e)
        {
            // Trigger
            _executeTriggerEvent.Set();
        }
    }

    public class ConnectionProxy<TService, TCallback> : ConnectionProxy<TService>
        where TService : class, IConnectionService
        where TCallback : class
    {
        // Fields
        private TCallback _callback = null;


        // Constructors
        public ConnectionProxy(TCallback callback, Binding binding, EndpointAddress adress)
            : base(new DuplexChannelFactory<TService>(callback, binding, adress))
        {
            #region Contracts

            if (callback == null) throw new ArgumentNullException();
            if (binding == null) throw new ArgumentNullException();
            if (adress == null) throw new ArgumentNullException();

            #endregion

            // Callback     
            _callback = callback;
        }


        // Properties
        public TCallback Callback
        {
            get
            {
                // Return
                return _callback;
            }
        }
    }
}
