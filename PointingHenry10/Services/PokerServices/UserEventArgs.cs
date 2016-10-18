using System;
using PointingHenry10.Models;

namespace PointingHenry10.Services.PokerServices
{
    public class UserEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}
