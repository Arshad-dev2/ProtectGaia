using System;
using ProtectGaia.Entities.MeetupEntity;

namespace ProtectGaia.Interface
{
    public interface IMeetupApi
    {
        public EventRequest UpcomingEvents();
        
    }
}
