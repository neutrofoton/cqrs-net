﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Events.Handlers
{
    public interface IEventListenerHandler
    {
        object Process(EventMessage @event);
    }
}
