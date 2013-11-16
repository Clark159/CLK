﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Communication
{
    public interface IDeviceStrategy<TAddress> 
        where TAddress : DeviceAddress
    {
        // Properties
        TAddress LocalAddress { get; }

        TAddress RemoteAddress { get; }


        // Methods        
        void Start();

        void Stop();

        IEnumerable<IDeviceCommandStrategy<TAddress>> GetAllCommandStrategy();


        // Events
        event Action<long> Ticked;
    }
}
