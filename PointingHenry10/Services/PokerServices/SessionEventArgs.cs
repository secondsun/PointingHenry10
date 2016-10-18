using System;
using PointingHenry10.Models;

namespace PointingHenry10.Services.PokerServices
{
    public class SessionEventArgs: EventArgs
    {
        public Session Session { get; set; }
    }
}
