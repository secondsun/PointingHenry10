using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
namespace PointingHenry10.Models
{
    // TODO http://stackoverflow.com/questions/28844518/bindablebase-vs-inotifychanged
    public class Session: BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name;}
            set {
                _name = value;
                //this.OnPropertyChanged(_name);
                //SetProperty(ref _name, value);
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
