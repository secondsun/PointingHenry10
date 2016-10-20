using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
namespace PointingHenry10.Models
{
    public class Session
    {
        public string Name { get; set; }
        public User CreatedBy { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
