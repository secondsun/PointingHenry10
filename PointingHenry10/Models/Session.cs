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
        private string _name;
        public string Name
        {
            get { return _name;}
            set {
                _name = value;
            }
        }

        private User _createdBy;
        public string CreatedBy
        {
            get { return _createdBy.Name; }
            set { _createdBy = new User() { Name = value };  }
        }

    }
}
