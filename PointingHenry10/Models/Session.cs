using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointingHenry10.Models
{
    public class Session
    {
        public string Name { get; set; }

        private User _createdBy;
        public string CreatedBy
        {
            get { return _createdBy.Name; }
            set { _createdBy = new User() { Name = value };  }
        }

    }
}
