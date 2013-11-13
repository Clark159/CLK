﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLK.Communication
{
    public interface IDevicePassiveCommandStrategy<TDeviceAddress, TRequest, TResponse> : IDeviceCommandStrategy<TDeviceAddress, TRequest, TResponse>
        where TDeviceAddress : DeviceAddress
        where TRequest : class
        where TResponse : class
    {
        // Events
        event Action<Guid, TDeviceAddress, TDeviceAddress, TRequest> ExecuteArrived;
    }
}
